import os
import numpy as np
import tensorflow as tf
import onnx
import tf2onnx
from sklearn.model_selection import train_test_split

import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt

from sklearn.metrics import confusion_matrix, classification_report
from imblearn.over_sampling import SMOTE

RANDOM_SEED = 42

dataset = 'testDataset.csv'
model_save_path = 'OldModel/WIPClassifier.hdf5'

NUM_CLASSES = 4

X_dataset = np.loadtxt(dataset, delimiter=',', dtype='float32', usecols=list(range(1, (13 * 11) + 1)))
y_dataset = np.loadtxt(dataset, delimiter=',', dtype='int32', usecols=(0))

oversample = SMOTE()

x_smote, y_smote = oversample.fit_resample(X_dataset, y_dataset)

X_train, X_test, y_train, y_test = train_test_split(x_smote, y_smote, train_size=0.80, random_state=RANDOM_SEED)


model = tf.keras.models.Sequential([
    tf.keras.layers.Input(shape=(13 * 11,)),
    tf.keras.layers.Dropout(0.1),
    tf.keras.layers.Dense(132, activation='relu'),
    tf.keras.layers.Dropout(0.2),
    tf.keras.layers.Dense(116, activation='relu'),
    tf.keras.layers.Dropout(0.4),
    tf.keras.layers.Dense( 58, activation='relu'),
    tf.keras.layers.Dense(NUM_CLASSES, activation='softmax')
])

cp_callback = tf.keras.callbacks.ModelCheckpoint(
    model_save_path, verbose=1, save_weights_only=False)

es_callback = tf.keras.callbacks.EarlyStopping(patience=20, verbose=1)

model.compile(
    optimizer='adam',
    loss='sparse_categorical_crossentropy',
    metrics=['accuracy']
)

history = model.fit(
    X_train,
    y_train,
    epochs=1000,
    batch_size=128,
    validation_data=(X_test, y_test),
    callbacks=[cp_callback, es_callback]
)

val_loss, val_acc = model.evaluate(X_test, y_test, batch_size=128)

model = tf.keras.models.load_model(model_save_path)

predict_result = model.predict(np.array([X_test[0]]))

# convert model to ONNX
'''onnx_model = keras2onnx.convert_keras(model,       # keras model
                         name="OculusClassifier",  # the converted ONNX model internal name
                         target_opset=9,           # the ONNX version to export the model to
                         channel_first_inputs=None # which inputs to transpose from NHWC to NCHW
                         )

onnx.save_model(onnx_model, "OculusClassifier.onnx")'''


tf.saved_model.save(model, "tmp_model")

os.system('python -m tf2onnx.convert --saved-model tmp_model --output "WIPClassifier.onnx"')


def print_confusion_matrix(y_true, y_pred, report=True):
    labels = sorted(list(set(y_true)))
    cmx_data = confusion_matrix(y_true, y_pred, labels=labels)

    df_cmx = pd.DataFrame(cmx_data, index=labels, columns=labels)

    fig, ax = plt.subplots(figsize=(7, 6))
    sns.heatmap(df_cmx, annot=True, fmt='g', square=False)
    ax.set_ylim(len(set(y_true)), 0)
    plt.show()

    if report:
        print('Classification Report')
        print(classification_report(y_test, y_pred))


Y_pred = model.predict(X_test)
y_pred = np.argmax(Y_pred, axis=1)

print_confusion_matrix(y_test, y_pred)

def plot_metric(history, metric):
    train_metrics = history.history[metric]
    val_metrics = history.history['val_'+metric]
    epochs = range(1, len(train_metrics) + 1)
    plt.plot(epochs, train_metrics)
    plt.plot(epochs, val_metrics)
    plt.title('Training and validation '+ metric)
    plt.xlabel("Epochs")
    plt.ylabel(metric)
    plt.legend(["train_"+metric, 'val_'+metric])
    plt.show()

plot_metric(history, 'loss')