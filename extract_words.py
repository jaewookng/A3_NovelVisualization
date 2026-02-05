import re
from collections import defaultdict

# https://www.w3schools.com/python/python_file_write.asp
# https://www.w3schools.com/python/python_regex.asp
with open("novel.txt") as f:
    words = re.findall(r'[a-z]+', f.read().lower())

# write every word into a new file ('w' overwrites if existing, creates if not)
with open("allwords.txt", 'w') as f:
    f.write('\n'.join(words))

# seek out unique words (key value == 1)
wordcount_dict = defaultdict(int)
for word in words:
    wordcount_dict[word] += 1

with open("uniquewords.txt", 'w') as f:
    for word in wordcount_dict:
        if wordcount_dict[word] == 1:
            f.write(word + '\n')

# https://www.geeksforgeeks.org/python/python-sort-python-dictionaries-by-key-or-value/
# a sorted defaultdict gives in order of increasing freq count
freqcount_dict = defaultdict(int)
for count in wordcount_dict.values():
    freqcount_dict[count] += 1


with open("wordfrequency.txt", 'w') as f:
    for freq in sorted(freqcount_dict):
        f.write(f"{freq}: {freqcount_dict[freq]}\n")
