## How to run

### Prerequisites
Run postgres in docker:
- `docker pull postgres`
- `docker run --name postgres-container -e POSTGRES_PASSWORD=mysecretpassword -e POSTGRES_DB=sendouts -p 5432:5432 -d postgres`

### Run the project
The project can be run directly from IDE using one of the profiles in launchSettings.json or with `dotnet run`.
The project to run is either of the APIs: `CRM.Deals.Api` or `CRM.Sendouts.Api`.

After starting the program, navigate to the browser, specifically to the swagger UI to send some requests.