// DOM Elements
const searchInput = document.getElementById('searchInput');
const searchButton = document.getElementById('searchButton');
const main = document.getElementById('main');
const searchContainer = document.getElementById('searchContainer');
const resultsContainer = document.getElementById('resultsContainer');
const options = document.querySelectorAll('.option');

// API Configuration
const BASE_URL = "https://localhost:7249";
const SEARCHES_API = BASE_URL + '/api/Search';
const RESULTS_API = BASE_URL + '/api/Search';

// API Services
const apiService = {
    async saveSearchQuery(query) {
        try {
            await fetch(SEARCHES_API, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ query })
            });
        } catch (error) {
            console.error('Error saving search query:', error);
        }
    },

    async getSearchResults(query) {
        try {
            const response = await fetch(`${RESULTS_API}?word=${encodeURIComponent(query)}`);
            if (response.ok) {
                const data = await response.json();
                if (Array.isArray(data) && data.length > 0) {
                    return data;
                }
            }
            return [];
        } catch (error) {
            console.error('Error fetching results:', error);
            return [];
        }
    },

    async getRecentSearches(limit = 5) {
        try {
            const response = await fetch(`${SEARCHES_API}?limit=${limit}`);
            if (response.ok) {
                const data = await response.json();
                if (Array.isArray(data)) {
                    return data;
                }
            }
            return [];
        } catch (error) {
            console.error('Error fetching recent searches:', error);
            return [];
        }
    }
};

// UI Services
const uiService = {
    displayResults(query, results) {
        resultsContainer.innerHTML = '';
        main.classList.add('mini-mode');
        document.title = `${query} - Searchify Results`;

       
        if (!results || results.length === 0) {
            this.showNoResults();
            return;
        }

        // If results is an array of URLs (strings), display each as a link
        if (typeof results[0] === 'string') {
            results.forEach(url => {
                const resultElement = document.createElement('div');
                resultElement.className = 'result-item';
                resultElement.innerHTML = `
                    <h3 class="result-title">
                        <a href="${url}" target="_blank" rel="noopener noreferrer">
                            ${url}
                        </a>
                    </h3>
                    <p class="result-url">${url}</p>
                `;
                resultsContainer.appendChild(resultElement);
            });
            return;
        }

        results.forEach(result => {
            const resultElement = document.createElement('div');
            resultElement.className = 'result-item';
            resultElement.innerHTML = `
                <h3 class="result-title">
                    <a href="${result.url}" target="_blank" rel="noopener noreferrer">
                        ${result.title}
                    </a>
                </h3>
                <p class="result-url">${result.url}</p>
                <p class="result-description">${result.description}</p>
            `;
            resultsContainer.appendChild(resultElement);
        });
    },

    showNoResults() {
        const noResultsElement = document.createElement('div');
        noResultsElement.className = 'loading';
        noResultsElement.textContent = 'No results found.';
        resultsContainer.appendChild(noResultsElement);
    },

    displayRecentSearches(searches) {
        const recentSearchesContainer = document.getElementById('recentSearches');
        recentSearchesContainer.innerHTML = '';

        if (searches.length > 0) {
            const label = document.createElement('div');
            label.className = 'recent-searches-label';
            label.textContent = 'Recent searches:';
            recentSearchesContainer.appendChild(label);

            searches.forEach(query => {
                const searchItem = document.createElement('div');
                searchItem.className = 'recent-search-item';
                searchItem.textContent = query;
                recentSearchesContainer.appendChild(searchItem);
            });
        }
    }
};

// Event Handlers
const eventHandlers = {
    handleSearch() {
        const query = searchInput.value.trim();
        if (query) {
            this.performSearch(query);
        }
    },

    async performSearch(query) {
        apiService.saveSearchQuery(query);
        const results = await apiService.getSearchResults(query);
        uiService.displayResults(query, results);
        this.refreshRecentSearches();
    },

    async refreshRecentSearches() {
        const recentSearches = await apiService.getRecentSearches();
        uiService.displayRecentSearches(recentSearches);
    }
};

// Event Listeners
searchButton.addEventListener('click', () => eventHandlers.handleSearch());
searchInput.addEventListener('keypress', (e) => {
    if (e.key === 'Enter') eventHandlers.handleSearch();
});

window.addEventListener('load', () => eventHandlers.refreshRecentSearches());

// Initialize Recent Searches
eventHandlers.refreshRecentSearches();