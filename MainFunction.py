from Models import *

def mainFunction(fileName, stockNumber, period):
    df = readData(fileName)
    m = createProphet()
    # m.add_country_holidays(country_name='Japan')
    # m.add_seasonality('weekly')
    m.fit(normalizeData(df, stockNumber))
    # print(getForecast(m, period))
    plotPrediction(stockNumber, m, period)
