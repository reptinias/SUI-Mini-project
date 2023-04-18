import pandas as pd
import pathlib
import os



tempList = [0] * 154
tempClassList = []
tempDataList = []
finishedDataset = []

path = r'C:\Users\mikael\PycharmProjects\SUI-Mini-project\NewDataFiles'
all_files = os.listdir(path)

li = []
print(all_files)
for filename in all_files:

    df = pd.read_csv(path + "/" + filename, index_col=None, header=0)
    li.append(df)

frame = pd.concat(li, axis=0, ignore_index=True)

df = frame.drop(["Timestamp", "Framecount", "SessionID", "Email"], axis=1)

for index, row in df.iterrows():
    tempList = tempList[14:]
    tempList.extend(df.iloc[index].values.flatten().tolist())
    for i in range(11):
        tempClassList.append(tempList[i*14])
        tempDataList.extend(tempList[i * 14 + 1:i * 14 + 14])

    finalclass = max(set(tempClassList), key=tempClassList.count)
    print(tempList)
    print(tempDataList)

    finalWindowVector = []
    finalWindowVector.append(finalclass)
    finalWindowVector.extend(tempDataList)

    finishedDataset.append(finalWindowVector)

    tempDataList.clear()
    tempClassList.clear()

df = pd.DataFrame(finishedDataset)
df.to_csv('testDataset.csv', index=False)