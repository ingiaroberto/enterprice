# Local containers

The Dockerfiles build each service independently from the repository root. They restore `RIDA.Enterprise.sln` and publish only the selected API project.

`docker-compose.local.yml` provides SQL Server and RabbitMQ as local dependencies. Credentials in this file are local-development placeholders only and must not be reused in shared environments.
