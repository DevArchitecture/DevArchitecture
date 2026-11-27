# Oracle Always Free VM deployment

Everything on one machine: the API, Postgres, Redis, and the four admin clients behind Caddy
with automatic HTTPS. No external accounts beyond Oracle Cloud.

## 1. Create the VM (Oracle Cloud console)

1. Compute → Instances → **Create instance**.
2. Image: **Ubuntu 24.04** (aarch64).
3. Shape: **VM.Standard.A1.Flex**, **2 OCPUs**, **12 GB** memory (the Always Free allowance).
   If you get "out of host capacity", try another availability domain or retry later.
4. Boot volume: the default 47 GB is enough.
5. Networking: keep the default VCN, and **assign a public IPv4 address**.
6. After creation, open the subnet's **Security List** and add ingress rules:
   - TCP **22** (SSH), source `0.0.0.0/0` or your IP
   - TCP **80** and **443**, source `0.0.0.0/0`
7. Note the instance's public IP.

## 2. Install

SSH into the VM and run:

```bash
sudo apt-get update && sudo apt-get install -y git
git clone https://github.com/DevArchitecture/DevArchitecture.git
cd DevArchitecture
sudo bash deploy/oracle/setup.sh
```

The script installs Docker, opens ports 80/443 in the VM firewall, generates
`deploy/oracle/.env` (database password, JWT key, domain) and starts the stack.

By default the domain is `<public-ip>.sslip.io`, which resolves to the VM and lets Caddy
obtain a free Let's Encrypt certificate. To use your own domain, set it before running:

```bash
DOMAIN=dev.example.com sudo -E bash deploy/oracle/setup.sh
```

and point an `A` record at the VM first.

## 3. Open it

```
https://<public-ip>.sslip.io/        landing page with links to all four clients
https://<public-ip>.sslip.io/vue/
https://<public-ip>.sslip.io/react/
https://<public-ip>.sslip.io/angular/
https://<public-ip>.sslip.io/blazor/
https://<public-ip>.sslip.io/healthz API health
```

Sign in with `admin@adminmail.com` / `Q1w212*_*`.

## What runs

| Container | Image | Purpose |
|-----------|-------|---------|
| `web` | Caddy 2 | TLS, serves the four SPAs, proxies `/api/*` to the API |
| `api` | `WebAPI/Dockerfile` (.NET 10) | ASP.NET Core API, Production mode |
| `db` | `postgres:17-alpine` | database, volume `db` |
| `redis` | `redis:7-alpine` | cache and token revocation |

The four clients are built with `API_BASE_URL=/api/v1`, so they talk to the same origin and
there is no CORS setup. `Database__AutoMigrate=true` applies the EF migrations on startup;
languages and translations come from `HasData`, and the admin user is created by
`OperationClaimCreatorMiddleware`.

## Operations

```bash
cd ~/DevArchitecture/deploy/oracle

sudo docker compose ps                  # status
sudo docker compose logs -f web api     # logs
sudo docker compose pull && sudo docker compose up -d --build   # update after git pull
sudo docker compose down                # stop (volumes are kept)
```

Data lives in the `db` Docker volume. To start over, `sudo docker compose down -v` (this
deletes all data).

## Notes

- Production mode disables Swagger and the fake-data seeding, as intended.
- A single API instance is assumed; Redis is present but the in-memory fallback would also work.
- The VM is idle-reclaimable by Oracle if CPU/network/memory stay below 20% over 7 days. A
  small monitor or real traffic avoids that; a demo that nobody visits may eventually be
  reclaimed.
- The first build compiles three Node clients and the Blazor app; expect 10-20 minutes on the
  free Ampere shape.
