# Contributing

We welcome contributions to the SOAD project. Here are some ways you can help:

- Report bugs
- Fix issues
- Add new features
- Improve documentation

## How to Contribute

1. Setup a python virtual environment

    ```
    python -m pyenv python3.12
    ```

2. Install the required packages:

    ```
    pip install -r requirements.txt
    ```

3. Initialize the database with fake data:

    ```
    python init_db.py
    ```

4. Start the frontend (React) server

    Create a file called `/trading-dashboard/.env.local` with this line:
    ```
    REACT_APP_API_URL=http://localhost:8000
    ```

    To prevent `package.json` from [unexpectedly changing](https://github.com/nodejs/corepack/issues/485) set this environment variable:
    ```
    COREPACK_ENABLE_AUTO_PIN=0
    ```

    Then:

    ```
    cd trading-dashboard
    yarn start
    ```

5. Start the python API (in a second terminal window)

    ```
    python main.py --mode api
    ```

## Code of Conduct

If you have any problems getting, feel free to create a Github issue.

Please follow our [Code of Conduct](https://github.com/r0fls/soad/blob/main/CODE_OF_CONDUCT.md).
