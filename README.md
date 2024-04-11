# StockAnalyze
This is a quite simple project to analyze the Japanese stock.
Data Source is from [株マップ.com](https://jp.kabumap.com/servlets/kabumap/Action?SRC=marketList/base).
https://jp.kabumap.com
https://jp.kabumap.com/servlets/kabumap/Action?SRC=marketList/base
## Overview

Belows are the explanation of python scripts.
     
     Analyze.py: Entrypoint of whole project
     Models.py: All methods
----
## User Guide
STEP 1. Download the csv.file from this [link](https://xgf.nu/MdWvm) and save in the same directory of this project. 
        
STEP 2. Input the following command in cmd or anaconda prompt to run this program:
    
    python Analyze.py stockNumber

STEP 3. If it turns out the following error,

    ModuleNotFoundError: No module named 'XXXXX'

such error can be solved by installing the corresponding package. Below is the example command:
    
     pip install XXXXX
