import re
import string
from collections import defaultdict
import os
import glob

# List of common English stopwords
STOPWORDS = {
    'a', 'an', 'the', 'and', 'or', 'but', 'if', 'because', 'as', 'what',
    'which', 'this', 'that', 'these', 'those', 'then', 'just', 'so', 'than',
    'such', 'when', 'while', 'of', 'at', 'by', 'for', 'with', 'about',
    'against', 'between', 'into', 'through', 'during', 'before', 'after',
    'above', 'below', 'to', 'from', 'up', 'down', 'in', 'out', 'on', 'off',
    'over', 'under', 'again', 'further', 'then', 'once', 'here', 'there',
    'when', 'where', 'why', 'how', 'all', 'any', 'both', 'each', 'few',
    'more', 'most', 'other', 'some', 'such', 'no', 'nor', 'not', 'only',
    'own', 'same', 'so', 'than', 'too', 'very', 'can', 'will', 'just', 'don',
    'should', 'now', 'i', 'me', 'my', 'myself', 'we', 'our', 'ours', 'ourselves',
    'you', 'your', 'yours', 'yourself', 'yourselves', 'he', 'him', 'his',
    'himself', 'she', 'her', 'hers', 'herself', 'it', 'its', 'itself', 'they',
    'them', 'their', 'theirs', 'themselves', 'am', 'is', 'are', 'was', 'were',
    'be', 'been', 'being', 'have', 'has', 'had', 'having', 'do', 'does', 'did',
    'doing', 'would', 'could', 'should', 'ought', 'i\'m', 'you\'re', 'he\'s',
    'she\'s', 'it\'s', 'we\'re', 'they\'re', 'i\'ve', 'you\'ve', 'we\'ve',
    'they\'ve', 'i\'d', 'you\'d', 'he\'d', 'she\'d', 'we\'d', 'they\'d', 'i\'ll',
    'you\'ll', 'he\'ll', 'she\'ll', 'we\'ll', 'they\'ll', 'isn\'t', 'aren\'t',
    'wasn\'t', 'weren\'t', 'hasn\'t', 'haven\'t', 'hadn\'t', 'doesn\'t', 'don\'t',
    'didn\'t', 'won\'t', 'wouldn\'t', 'shan\'t', 'shouldn\'t', 'can\'t', 'cannot',
    'couldn\'t', 'mustn\'t', 'let\'s', 'that\'s', 'who\'s', 'what\'s', 'here\'s',
    'there\'s', 'when\'s', 'where\'s', 'why\'s', 'how\'s'
}

def preprocess_text(text):
    """
    Preprocess the text:
    1. Convert to lowercase
    2. Remove punctuation
    3. Tokenize into words
    4. Remove stopwords
    
    Returns a list of processed words
    """
    # Convert to lowercase
    text = text.lower()
    
    # Remove punctuation
    text = text.translate(str.maketrans('', '', string.punctuation))
    
    # Tokenize into words
    words = text.split()
    
    # Remove stopwords
    words = [word for word in words if word not in STOPWORDS]
    
    return words

def build_inverted_index(files):
    """
    Build an inverted index from a list of files where each file follows the format:
    - First line contains the URL
    - Subsequent lines contain the content
    
    Returns a dictionary mapping words to a dictionary of URLs and their occurrence counts
    """
    inverted_index = defaultdict(dict)
    
    for file_content in files:
        lines = file_content.strip().split('\n')
        if not lines:
            continue
            
        url = lines[0]
        content = ' '.join(lines[1:])
        
        # Preprocess the content
        words = preprocess_text(content)
        
        # Count word occurrences for this URL
        word_counts = {}
        for word in words:
            if word:  # Ignore empty strings
                word_counts[word] = word_counts.get(word, 0) + 1
        
        # Update the inverted index
        for word, count in word_counts.items():
            inverted_index[word][url] = count
    
    return inverted_index

def format_inverted_index(inverted_index):
    """
    Format the inverted index according to the specified format:
    word url1:count1,url2:count2,...;
    """
    output_lines = []
    
    for word, url_counts in sorted(inverted_index.items()):
        url_count_strs = [f"{url}:{count}" for url, count in url_counts.items()]
        output_line = f"{word} {','.join(url_count_strs)};"
        output_lines.append(output_line)
    
    return '\n'.join(output_lines)

def process_files(file_contents):
    """
    Process a list of file contents and return the formatted inverted index
    """
    inverted_index = build_inverted_index(file_contents)
    return format_inverted_index(inverted_index)

# Example usage with mock data
example_files = [
    """https://example.com
    This is an example document about programming and algorithms.
    Programming requires practice and dedication to learn.
    """,
    
    """https://blog.example.com/python
    Python is a popular programming language for data science.
    Many beginners start learning programming with Python because it's easy to read.
    """,
    
    """https://techsite.com/coding
    Coding bootcamps teach programming skills in a short period.
    Algorithms and data structures are fundamental concepts in programming.
    """
]

# Generate the inverted index
result = process_files(example_files)
print(result)

# For handling large datasets (e.g., 100,000 URLs), consider these optimizations:
"""
Optimization suggestions for large datasets:

1. Parallel processing: 
   - Use multiprocessing to process files in parallel
   - Divide the files into batches and process each batch in a separate process

2. Memory-efficient data structures:
   - Use generators and iterators to process files one at a time
   - Consider using compressed data structures like tries for storing the inverted index

3. Database storage:
   - Store the inverted index in a database (e.g., SQLite, MongoDB) for large datasets
   - Implement incremental processing to update the index without reprocessing everything

4. Memory mapping:
   - Use memory-mapped files for very large datasets that don't fit in memory
   - Process the files in chunks to avoid loading everything into memory at once

5. Efficient algorithms:
   - Use hash tables for fast lookups
   - Implement batch processing to reduce overhead

6. Output optimization:
   - Stream the output to a file instead of storing it in memory
   - Use compressed file formats for storage efficiency
"""


def read_files_from_folder(folder_path):
    """
    Read all text files from the specified folder.
    """
    file_contents = []
    
    # Get all text files in the directory
    file_paths = glob.glob(os.path.join(folder_path, "*.*"))
    
    for file_path in file_paths:
        try:
            with open(file_path, 'r', encoding='utf-8') as file:
                content = file.read()
                file_contents.append(content)
        except Exception as e:
            print(f"Error reading file {file_path}: {e}")
    
    return file_contents

def main(folder_path, output_file=None):
    """
    Main function to process files from a folder and generate an inverted index.
    """
    # Read files from the folder
    file_contents = read_files_from_folder(folder_path)
    
    if not file_contents:
        print(f"No files found in {folder_path}")
        return
    
    print(f"Processing {len(file_contents)} files...")
    
    # Generate the inverted index
    result = process_files(file_contents)
    
    # Output the result
    if output_file:
        with open(output_file, 'w', encoding='utf-8') as f:
            f.write(result)
        print(f"Inverted index written to {output_file}")
    else:
        print(result)