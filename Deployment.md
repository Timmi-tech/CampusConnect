# Deployment Guide: Deploying .NET App to Render

This guide describes the steps taken to deploy the Campus Connect .NET application to [Render](https://render.com/).

---

## 1. Prepare the Codebase

- Ensure all code is committed and pushed to your GitHub repository.
- Confirm your `Dockerfile` and `entrypoint.sh` are present in the `ChatSystem-1.Api` directory.
- Make sure your migrations are in `ChatSystem-1.Infrastructure`.

---

## 2. Create the Entrypoint Script

Create a file named `entrypoint.sh` in `ChatSystem-1.Api` with the following content:

```sh
#!/bin/sh
set -e

# Run EF Core migrations
dotnet ef database update --project ../ChatSystem-1.Infrastructure --startup-project . --no-build

# Start the application
exec dotnet ChatSystem-1.Api.dll
```

Commit and push this file to your repository.

---

## 3. Update the Dockerfile

Modify the final stage of your `ChatSystem-1.Api/Dockerfile` to:

```dockerfile
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY entrypoint.sh .
RUN chmod +x entrypoint.sh

# Install dotnet-ef tool
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

ENTRYPOINT ["./entrypoint.sh"]
```

---

## 4. Set Up Environment Variables

Add your environment variables in Render's **Environment** tab:

| Key                                   | Value                                 |
|----------------------------------------|---------------------------------------|
| ConnectionStrings__PostgresConnection  | your converted PostgreSQL connection string |
| JWT_SECRET                            | your JWT secret                       |
| Cloudinary__CloudName                  | your Cloudinary CloudName             |
| Cloudinary__ApiKey                     | your Cloudinary ApiKey                |
| Cloudinary__ApiSecret                  | your Cloudinary ApiSecret             |

> **Note:**  
> Make sure the double underscores `__` match your nested config structure.

---

## 5. Provision the Database

- Create a PostgreSQL database on Render or another provider.
- Copy the connection string and add it as an environment variable in Render.

---

## 6. Create the Render Web Service

- Go to the Render dashboard and click **"New Web Service"**.
- Connect your GitHub repository.
- Set the root directory to `ChatSystem-1.Api` (if your Dockerfile and entrypoint.sh are in that folder).
- Render will auto-detect your Dockerfile.
- Deploy the service.

---

## 7. Verify Deployment

- Once deployed, use the Render-provided URL to test your API endpoints.
- Check logs for any errors and confirm migrations ran successfully.

---

## 8. Troubleshooting

- Ensure `entrypoint.sh` is present and committed in `ChatSystem-1.Api`.
- Make sure `.dockerignore` does not exclude `entrypoint.sh`.
- If migrations fail, check your connection string and EF Core setup.

---

**Deployment complete!**