
import os
import pandas as pd # type: ignore
from collections import Counter
import glob
from itertools import islice

script_dir = os.path.dirname(os.path.abspath(__file__)) 
os.chdir(script_dir) 
 

def read_file_in_chunks(file_path, chunk_size=100):
    with open(file_path, 'r', encoding='utf-8') as f:
        while True:
            lines = list(islice(f, chunk_size))
            if not lines:
                break
            yield lines  


def count_frequencies(chunk):
    counter = Counter()
    for line in chunk:
        counter[line.strip('"\n ').split('Error: ')[-1]] += 1  
    return counter

def most_common_error_codes(file_path,N):
    total_counter = Counter()
    
   
    for i, chunk in enumerate(read_file_in_chunks(file_path, chunk_size=1000)):   # O(M) M = lines in the file
        part_counter = count_frequencies(chunk)
        total_counter.update(part_counter)
           
    return total_counter.most_common(N)  #O(n log(N)) - (sort by max heap)


print(most_common_error_codes("logs.txt",5))




