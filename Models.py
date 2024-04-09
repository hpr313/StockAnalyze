import pandas as pd
from prophet import Prophet
import matplotlib.pyplot as plt

def readData(fileName):
    df = pd.read_csv(f'{fileName}', encoding='utf-8')
    return df


def formatDatetime(dataframe):
    dataframe['StockDatetime'] = pd.to_datetime(dataframe['StockDatetime'], format='%Y-%m-%d')


def getData(dataframe, stockNumber):
    df = dataframe.loc[dataframe['Number'] == stockNumber]
    return df


def selectColumns(dataframe, cols):
    df = dataframe[cols]
    return df


def columnRename(dataframe):
    df = dataframe.rename(columns={'StockDatetime': 'ds', 'StockValue': 'y'})
    return df


def getForecast(m, period):
    # formatDatetime(dataframe)
    # dataframe = getData(dataframe, stockNumber)
    # dataframe = selectColumns(dataframe, ['StockValue', 'StockDatetime'])
    # dataframe = columnRename(dataframe)
    return createForecast(m, period)


def normalizeData(dataframe, stockNumber):
    formatDatetime(dataframe)
    dataframe = getData(dataframe, stockNumber)
    dataframe = selectColumns(dataframe, ['StockValue', 'StockDatetime'])
    dataframe = columnRename(dataframe)
    return dataframe


def createProphet():
    return Prophet(weekly_seasonality=False, seasonality_mode='additive', daily_seasonality=True)


def createForecast(m, period):
    # m.add_country_holidays(country_name='Japan')
    # m.fit(dataframe)
    future = m.make_future_dataframe(periods=period)
    forecast = m.predict(future)
    # return forecast[['ds', 'yhat', 'yhat_lower', 'yhat_upper']]
    return forecast

def plotPrediction(stockNumber, m, period):
    prediction = getForecast(m, period)
    # m.plot(createForecast(m, period))
    m.plot(prediction)
    plt.title(f'{stockNumber}', loc='center', y=0.988)
    plt.xlabel("Date")
    plt.ylabel("Close")
    # plt.show()
    m.plot_components(createForecast(m, period))
    plt.show()

def mainFunction(fileName, stockNumber, period):
    df = readData(fileName)
    m = createProphet()
    # m.add_country_holidays(country_name='Japan')
    # m.add_seasonality('weekly')
    m.fit(normalizeData(df, stockNumber))
    # print(getForecast(m, period))
    plotPrediction(stockNumber, m, period)

if __name__ == "__main__":
    mainFunction()
