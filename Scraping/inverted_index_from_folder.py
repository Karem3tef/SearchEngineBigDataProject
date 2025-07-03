#!/usr/bin/env python3
"""
Inverted Index Builder - Command Line Tool

This script builds an inverted index from a collection of text files in a folder.
Each file should have the URL as the first line and the content in subsequent lines.

Usage:
    python inverted_index_from_folder.py input_folder [output_file]

Arguments:
    input_folder - Path to the folder containing text files
    output_file  - Optional path to save the output (if not provided, prints to console)
"""

import os
import sys
from inverted_index_builder import main

if __name__ == "__main__":
    # Parse command line arguments
    if len(sys.argv) < 2:
        print("Usage: python inverted_index_from_folder.py input_folder [output_file]")
        sys.exit(1)

    input_folder = sys.argv[1]
    output_file = sys.argv[2] if len(sys.argv) > 2 else None
    
    # Check if input folder exists
    if not os.path.isdir(input_folder):
        print(f"Error: The input folder '{input_folder}' does not exist.")
        sys.exit(1)
    
    # Run the main function
    main(input_folder, output_file)