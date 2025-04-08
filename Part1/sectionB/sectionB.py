from concurrent.futures import ThreadPoolExecutor
import threading
import pandas as pd # type: ignore
import os
import glob
from collections import defaultdict
script_dir = os.path.dirname(os.path.abspath(__file__)) 
os.chdir(script_dir) 

lock = threading.Lock()
all_hourly_averages = []


def integrity_check(df):
    
    df['timestamp'] = pd.to_datetime(df['timestamp'], errors='coerce')

    df['value'] = pd.to_numeric(df['value'], errors='coerce')

    df = df.dropna(subset=['timestamp', 'value'])

    df = df.drop_duplicates(subset=['timestamp', 'value'], keep='first')

    return df

def average_for_hour(file_name):

    # df = pd.read_csv(file_name)

    ext = os.path.splitext(file_name)[1].lower()

    if ext == '.csv':
        df = pd.read_csv(file_name)
    elif ext == '.parquet':
        df = pd.read_parquet(file_name)
    else:
        raise ValueError(f"Unsupported file: {file_name}")

    df=integrity_check(df)
    #hour without minutes
    df['hour'] = df['timestamp'].dt.floor('h')  

    hourly_avg = df.groupby('hour')['value'].mean()
    return hourly_avg

def process_file(file):
    try:
        hourly_avg = average_for_hour(file)

        # update all hourly average
        with lock:
            all_hourly_averages.append(hourly_avg)
    except Exception as e:
        print(f"error file: {file}: {e}")


def average_for_all_files(file_name,folder_name):

    extension = os.path.splitext(file_name)[1][1:]

    files = glob.glob(f"{folder_name}_{extension}/time_series_*.{extension}")
    # files = glob.glob(f"daily_files/time_series_*.csv")

    if not files:
        print("files not found")
        return
    
    # without threads

    # for file in glob.glob(f"daily_files/time_series_*.csv"):
    #     dayly_avg = average_for_hour(file)
    #     daily_dataframes.append(dayly_avg)

    with ThreadPoolExecutor(max_workers=10) as executor:
        executor.map(process_file, files)

    if all_hourly_averages:
        final_hourly_avg = pd.concat(all_hourly_averages)
        if isinstance(final_hourly_avg, pd.Series):
            final_hourly_avg = final_hourly_avg.reset_index()
            final_hourly_avg.columns = ['hour', 'value']  # שינוי שמות העמודות אם צריך

        if extension == 'csv':
            final_hourly_avg.to_csv("final_avg_output.csv")
            print("save file - CSV.")
        elif extension == 'parquet':

            final_hourly_avg.to_parquet("final_avg_output.parquet")
            
            print("save file - Parquet.")
        else:
            print("Unsupported file")
        # final_hourly_avg.to_csv("final_avg_output.csv")
    else:
        print("No data found")

def split_file(file_name, folder_name = 'daily_files'):
    # df = pd.read_csv(file_name)

    ext = os.path.splitext(file_name)[1].lower()

    if ext == '.csv':
        df = pd.read_csv(file_name)
        file_type = 'csv'
    elif ext == '.parquet':
        df = pd.read_parquet(file_name)
        file_type = 'parquet'
    else:
        raise ValueError(f"Unsupported file: {file_name}")

    df=integrity_check(df)

    # date without hour
    df['date'] = df['timestamp'].dt.date
    full_folder_name = f"{folder_name}_{file_type}"
    if not os.path.exists(full_folder_name):
        os.makedirs(full_folder_name)

    # unique date(day) for split by day
    unique_dates = df['date'].unique()

    for date in unique_dates:
       
        daily_df = df[df['date'] == date].drop(columns=['date'])
        
        new_file_name = os.path.join(full_folder_name, f'{file_name.split('.')[0]}_{date}.{file_type}')

        if ext == '.csv':
            daily_df.to_csv(new_file_name, index=False)
        elif ext == '.parquet':
            daily_df.to_parquet(new_file_name, index=False)
        

file_name = 'time_series.csv'
folder_name = 'daily_files'
split_file(file_name,folder_name)
average_for_all_files(file_name,folder_name)


# df = pd.read_csv("time_series.csv")
# df.to_parquet("time_series.parquet", engine="pyarrow", index=False)


################################## stream ########################################################

# hourly_data = defaultdict(lambda: [0, 0]) #sum, count

# def update_hourly_avg(new_data):
#     timestamp = pd.to_datetime(new_data['timestamp'])
#     hour = timestamp.replace(minute=0, second=0, microsecond=0)
#     value = new_data['value']

#     # עדכון הסכום והספירה
#     #avg
#     #hourly_data[hour][0] = ((hourly_data[hour][0]*hourly_data[hour][1]) + value) / (hourly_data[hour][1] + 1)

#     hourly_data[hour][0] += value  # סכום הערכים
#     hourly_data[hour][1] += 1  # ספירת הערכים

    
#     # חישוב הממוצע השעתי
#     average = hourly_data[hour][0] / hourly_data[hour][1]
#     print(f"Updated average for {hour}: {average}")

# עדכון הממוצע עם נתון חדש
# new_data = {'timestamp': '2025-06-02 01:05:00', 'value': 50.0}
# update_hourly_avg(new_data)

