
# 🔍 Search Engine - Big Data Project

A distributed search engine system that integrates **Python web scraping**, **.NET Web API**, **SQL Server**, and **Hadoop HDFS** to demonstrate full-stack big data processing using inverted indexing.

---

## 📖 Overview

This project was developed as part of a university Big Data course. It demonstrates how to build a scalable and efficient search engine by combining modern technologies:

- Scrape and preprocess data using Python
- Store structured content in SQL Server
- Build and store inverted index on HDFS using Hadoop
- Serve search functionality via a .NET Web API

---

## 🧰 Technologies Used

| Component            | Technology                |
|----------------------|----------------------------|
| Web Scraping         | Python (Requests, BeautifulSoup) |
| Backend API          | ASP.NET Core Web API       |
| Data Storage         | SQL Server                 |
| Big Data Indexing    | Hadoop HDFS                |
| Indexing Algorithm   | Inverted Index (MapReduce) |
| Architecture         | Onion Architecture         |

---

## 🚀 Features

- 🌐 Python web scraper for crawling and extracting article content
- 🗃️ Stores crawled data in SQL Server
- 🧠 Generates an inverted index using Hadoop HDFS
- 🔍 Keyword-based search with ranked results
- 📄 Search results with pagination
- 🔁 Clean, modular architecture (Domain, Application, Infrastructure, API)

---

## 📁 Project Structure

```

SearchEngineBigDataProject/
├── Scraper/                    → Python scraping scripts
├── SearchEngine.API/           → ASP.NET Core API
├── SearchEngine.Application/   → Business logic and services
├── SearchEngine.Domain/        → Core domain models
├── SearchEngine.Infrastructure/→ SQL Server & HDFS interaction
└── HadoopInvertedIndex/        → Java/Python code for MapReduce indexing

````

---

## 🔧 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- SQL Server instance
- Python 3.8+
- Hadoop (HDFS) running locally or remotely

---

### 1. Run the Web Scraper (Python)

Navigate to the scraper directory and run:

```bash
cd Scraper
python scrape.py
````

> This will scrape articles and insert structured data into SQL Server.

---

### 2. Build Inverted Index (Hadoop)

Use the Hadoop script provided to generate the inverted index from the saved documents.

```bash
hadoop jar inverted-index.jar input_folder output_folder
```

Or if using Python with Hadoop Streaming:

```bash
hadoop jar hadoop-streaming.jar \
    -input /searchengine/data \
    -output /searchengine/index \
    -mapper mapper.py \
    -reducer reducer.py
```

---

### 3. Run the .NET API

Configure `appsettings.json` in `SearchEngine.API`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Your_SQL_Server_Connection_String"
},
"HdfsSettings": {
  "Host": "http://localhost:9870",
  "BasePath": "/searchengine/index"
}
```

Then run the API:

```bash
dotnet run --project SearchEngine.API
```

Visit Swagger: `https://localhost:5001/swagger`

---

## 🔍 Example Search API

```http
GET /api/search?query=big+data&page=1&pageSize=10
```

Returns paginated, ranked results based on the inverted index stored in HDFS.

---

## 📬 Contact

**Author:** Mahmoud Amin
**GitHub:** [@MahmoudAmin5](https://github.com/MahmoudAmin5)
**Email:** [mahmoud886.amin@gmail.com](mahmoud886.amin@gmail.com)

---

---

## 🙌 Contributions

Contributions are welcome!
Feel free to open issues or submit pull requests for improvements.

