# from MainFunction import *
from Models import *
import sys

fileName = 'JapanStock.csv'
# stockNumber = 4595
stockNumber = int(sys.argv[1])
# print(stockNumber)
predictDays = 30
print(sys.argv)
mainFunction(fileName, stockNumber, predictDays)
