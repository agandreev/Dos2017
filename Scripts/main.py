from joblib import load
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import argparse

attacks = {
    '75.127.97.72' : [53, 118, 142, 170, 188, 993, 1033, 1165],
    '97.74.144.108' : [208, 426, 543, 560, 920, 947],
    '74.63.40.21' : [177],
    '208.113.162.153' : [209],
    '69.84.133.138' : [278],
    '67.220.214.50' : [360],
    '69.192.24.88' : [493, 827, 1163],
    '203.73.24.75' : [549],
    '74.55.1.4' : [662, 813, 1259],
    '97.74.104.201' : [687]
}

def LoadModel(path):
    clf = load(path)
    return clf

def LoadSet(path, server):
    data = pd.read_csv(path, index_col='No.')
    data = data[((data.Destination == server) &
                 ((data.Protocol == 'TCP') | (data.Protocol == 'HTTP')))]
    #print(len(data))
    return data

def CwdAndReqCount(data):
    Cwd = list()
    Req = list()
    period = data.iloc[0].Time
    cwd = 0
    req = 0
    for i in range(len(data)):
        line = data.iloc[i]
        if (line.Time % period < 30):
            # if (line.Protocol == 'TCP') and (('Len=0' in line.Info) or not('Len=' in line.Info)):
            if (('Len=0' in line.Info)):
                cwd += 1
            elif (line.Protocol == 'HTTP'):
                req += 1
        else:
            Cwd.append(cwd)
            Req.append(req)
            period = line.Time
            cwd = 0
            req = 0
            if (line.Protocol == 'TCP') and ('Len=0' in line.Info):
                cwd += 1
            elif (line.Protocol == 'HTTP'):
                req += 1
    Cwd = np.array(Cwd)
    Req = np.array(Req)

    new_data = pd.DataFrame({'Cwd': Cwd,
                             'Req': Req})
    return new_data

def RCount(new_data):
    R = new_data.Cwd * new_data.Cwd / (new_data.Req + 1)
    new_data = pd.concat([new_data, R], axis=1)
    new_data = new_data.rename(columns={0: 'R'})
    return new_data

def CCount(new_data):
    C = list()
    r = 0
    alpha = 0.01
    for i in range(len(new_data)):
        C.append(new_data[:i].R.mean())
    C = pd.DataFrame(np.array(C))
    new_data = pd.concat([new_data, C], axis=1)
    new_data = new_data.rename(columns={0: 'C'})
    new_data.C = new_data.C.fillna(0)
    return new_data

def BCount(new_data):
    B = list()
    for i in range(len(new_data)):
        B.append(new_data[:i].C.max())
    B = pd.DataFrame(np.array(B))
    new_data = pd.concat([new_data, B], axis=1)
    new_data = new_data.rename(columns={0: 'B'})
    new_data.B = new_data.B.fillna(0)
    return new_data

def SCount(new_data):
    S = list()
    s = 0
    for i in range(len(new_data)):
        line = new_data.iloc[i]
        # s = s + line.R - line.B
        s = line.R - line.B
        # s = s + line.R.mean() - line.B.mean()
        S.append(s)
    S = pd.DataFrame(np.array(S))
    new_data = pd.concat([new_data, S], axis=1)
    new_data = new_data.rename(columns={0: 'S'})
    new_data.S[new_data.S < 0] = 0
    return new_data

def Predict(clf, new_data):
    pred = clf.predict(new_data)
    return pred

def SlicesToTime(data, pred, server):

    per = 0
    period = data.iloc[0].Time
    pred_data = list()

    for i in range(len(data)):
        line = data.iloc[i]
        if (line.Time % period < 30):
            if (len(pred) - 1 < per):
                per -= 1
            pred_data.append(pred[per])
        else:
            per += 1
            #print(period)
            period = line.Time
            pred_data.append(pred[per - 1])

    pred_data = np.array(pred_data)
    data['Y'] = pred_data
    Plotting(data, server)

def Plotting(data, server):
    #plt.figure(figsize=(8, 6), dpi=60)
    plt.plot(data.Time, data.Y, label="Prediction")
    plt.title(server)
    plt.xlabel("Time")
    plt.ylabel("Prediction")
    plt.scatter(np.array(attacks[server])*60, [0] * len(attacks[server]), label="Attack", color="red")
    plt.legend()
    #plt.savefig(f"{server.replace('.', '_')}.png")
    plt.savefig("graph.png")
    plt.clf()

def Logging(message):
    print(message)
    print()

def Start(server):
    clf = LoadModel("../../../Scripts/model.joblib")
    #Logging("Модель загружена")
    data = LoadSet("../../../Scripts/data.csv", server)
    #Logging("Сет загружен")

    new_data = CwdAndReqCount(data)
    #Logging("Cwd and Req's counted")
    new_data = RCount(new_data)
    #Logging("R's counted")
    new_data = CCount(new_data)
    #Logging("C's counted")
    new_data = BCount(new_data)
    #Logging("B's counted")
    new_data = SCount(new_data)
    #Logging("S's counted")

    predictions = Predict(clf, new_data)
    #Logging("Predictions made")
    SlicesToTime(data, predictions, server)
    Logging("Plot is ready")

if __name__ == "__main__":
    import warnings
    warnings.filterwarnings('ignore')
    #start_time = datetime.now()
    #Start('97.74.144.108')
    #print(datetime.now() - start_time)
    #print("\a")

    arg_parse = argparse.ArgumentParser()
    arg_parse.add_argument('server')
    arguments = arg_parse.parse_args()
    #print(arguments.server)
    Start(arguments.server)