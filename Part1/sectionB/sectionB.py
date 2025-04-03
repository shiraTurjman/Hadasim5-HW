import pandas as pd # type: ignore
import os
import glob
from collections import defaultdict
script_dir = os.path.dirname(os.path.abspath(__file__))  # תיקיית הסקריפט
os.chdir(script_dir) 

def validation(file_path):
    
    df = pd.read_csv(os.path.join(os.getcwd(), 'sectionB', file_path))
    #בדיקת כפילויות
    # duplicates = df[df.duplicated()]
    # print(duplicates)
    #פורמט לא נכון 
    # df['timestamp'] = pd.to_datetime(df['timestamp'], format='%d/%m/%Y %H:%M', errors='coerce')
    # invalid_dates = df[df['timestamp'].isna()]
    # print(invalid_dates)
    #בלי ערך נכון 
    # invalid_values = df[pd.to_numeric(df['value'], errors='coerce').isna()]
    # print(invalid_values)
    #מיון לפי תאריכים 
    # df = df.sort_values(by='timestamp')

def integrity_check(df):
    # המרת העמודה Timestamp לפורמט datetime
    # format='%d/%m/%Y %H:%M',
    df['timestamp'] = pd.to_datetime(df['timestamp'], errors='coerce',dayfirst=True)

    # המרת העמודה 'value' למספרים, כל ערך שאינו נומרי יהפוך ל-NaN
    df['value'] = pd.to_numeric(df['value'], errors='coerce')


    #להוריד ערכים חסרים
    df = df.dropna(subset=['timestamp', 'value'])
    return df

def average_for_hour(file_name):

    df = pd.read_csv(file_name)
    #df = pd.read_parquet(file_name)
    df=integrity_check(df)
    # יצירת עמודה חדשה שמתארת את השעה (תוך שמירה רק על התאריך והשעה)
    df['hour'] = df['timestamp'].dt.floor('h')  # "floor" כדי לשמור רק על השעה (מינימום 00:00)

    # חישוב ממוצע לכל שעה
    hourly_avg = df.groupby('hour')['value'].mean()

    # הצגת הממוצע לפי שעה
    return hourly_avg

def average_for_all_files(base_name,folder_name):
    daily_dataframes = []
    for file in glob.glob(f"daily_files/time_series_*.csv"):
        dayly_avg = average_for_hour(file)
        daily_dataframes.append(dayly_avg)

    if daily_dataframes:
        final_hourly_avg = pd.concat(daily_dataframes)
        # שמירה או המשך טיפול בנתונים המאוחדים
        final_hourly_avg.to_csv("final_avg_output.csv")
    else:
        print("files not found.")

def split_file(file_name, folder_name = 'daily_files'):
    df = pd.read_csv(file_name)

    df=integrity_check(df)

    # גרימת עמודת תאריך בלבד (בלי שעה)
    df['date'] = df['timestamp'].dt.date

    # יצירת תיקיה חדשה לשמירה (אם היא לא קיימת)
    
    if not os.path.exists(folder_name):
        os.makedirs(folder_name)

    # רשימה של כל התאריכים הייחודיים בקובץ
    unique_dates = df['date'].unique()

    # חיתוך והצלה של קבצים לפי יום
    for date in unique_dates:
        # פילטר את הנתונים של אותו יום
        daily_df = df[df['date'] == date].drop(columns=['date'])
        
        # יצירת שם קובץ שמתאים לתאריך בתוך התיקיה החדשה
        new_file_name = os.path.join(folder_name, f'{file_name.split('.')[0]}_{date}.csv')
        
        # שמירת הקובץ החדש
        daily_df.to_csv(new_file_name, index=False)

file_name = 'time_series.csv'
folder_name = 'daily_files'
split_file(file_name,folder_name)
average_for_all_files(file_name,folder_name)




################################## stream ########################################################

hourly_data = defaultdict(lambda: [0, 0]) #sum, count

def update_hourly_avg(new_data):
    timestamp = pd.to_datetime(new_data['timestamp'])
    hour = timestamp.replace(minute=0, second=0, microsecond=0)
    value = new_data['value']

    # עדכון הסכום והספירה
    #avg
    #hourly_data[hour][0] = ((hourly_data[hour][0]*hourly_data[hour][1]) + value) / (hourly_data[hour][1] + 1)

    hourly_data[hour][0] += value  # סכום הערכים
    hourly_data[hour][1] += 1  # ספירת הערכים

    
    # חישוב הממוצע השעתי
    average = hourly_data[hour][0] / hourly_data[hour][1]
    print(f"Updated average for {hour}: {average}")

# עדכון הממוצע עם נתון חדש
# new_data = {'timestamp': '2025-06-02 01:05:00', 'value': 50.0}
# update_hourly_avg(new_data)
