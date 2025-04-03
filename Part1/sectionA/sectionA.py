
import os
import pandas as pd # type: ignore
from collections import Counter
import glob

script_dir = os.path.dirname(os.path.abspath(__file__))  # תיקיית הסקריפט
os.chdir(script_dir) 

#פונקציה שמחלקת את הקובץ הגדול לקבצים קטנים 
def split_file(input_file, lines_per_file=1000):
    with open(input_file, 'r', encoding='utf-8') as file:
        part = 1
        lines = []
        
        for line in file:
            # parts = line.split("Error: ")
            # if len(parts) > 1:
            #     lines.append(parts)
            lines.append(line)
            if len(lines) >= lines_per_file:
                write_to_file(lines, input_file, part)
                lines = []
                part += 1
        
        # כתיבת השורות האחרונות אם נשארו
        if lines:
            write_to_file(lines, input_file, part)

def write_to_file(lines, input_file, part,output_dir="split_logs"):
    """פונקציה כותבת חלק לקובץ חדש"""
    os.makedirs(output_dir, exist_ok=True)
    output_file = os.path.join(output_dir, f"{input_file.split('.')[0]}_part{part}.txt")

    with open(output_file, 'w', encoding='utf-8') as f:
        f.writelines(lines)
    print(f"new file: {output_file}")


#סופר את השכיחות בקובץ ספציפי
def count_frequencies(file_path):
    counter = Counter()
    with open(file_path, 'r', encoding='utf-8') as f:
        for line in f:
            counter[line.strip()] += 1  # שורה ללא רווחים מיותרים
    return counter


def process_all_parts(base_filename):
    # """מעבד את כל חלקי הקובץ ומחבר את התוצאות"""
    total_counter = Counter()
    
    # מחפש את כל הקבצים שמתחילים בשם הבסיסי (logs.txt_partX.txt)
    for file in glob.glob(f"split_logs/{base_filename}_part*.txt"):
        print(file)
        print(f"work {file}...")
        part_counter = count_frequencies(file)
        total_counter.update(part_counter)  # מחבר את התוצאות
    
    return total_counter

def most_common_error_codes(file_path,N):
    split_file(file_path,10000)
    final_counts = process_all_parts(file_path.split('.')[0])
    print()
    return final_counts.most_common(N)  

print(most_common_error_codes("logs.txt",2))