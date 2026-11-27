# Builds the four admin clients and serves them together with the API behind Caddy.

FROM node:22-bookworm-slim AS js
ARG API_BASE_URL=/api/v1
ENV VITE_API_BASE_URL=$API_BASE_URL
WORKDIR /src/clients

COPY clients/vue-admin/package.json clients/vue-admin/package-lock.json vue-admin/
COPY clients/react-admin/package.json clients/react-admin/package-lock.json react-admin/
COPY clients/angular-admin/package.json clients/angular-admin/package-lock.json angular-admin/
RUN cd vue-admin && npm ci
RUN cd react-admin && npm ci
RUN cd angular-admin && npm ci

COPY clients/vue-admin vue-admin
COPY clients/react-admin react-admin
COPY clients/angular-admin angular-admin
RUN cd vue-admin && npm run build -- --base=/vue/
RUN cd react-admin && npm run build -- --base=/react/
RUN cd angular-admin && \
    sed -i "s|https://localhost:5101/api/v1|$API_BASE_URL|g" src/environments/environment.ts && \
    npx ng build --base-href /angular/

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS blazor
WORKDIR /src
COPY . .
RUN dotnet publish clients/blazor-admin/Blazor.Admin.csproj -c Release -o /out
RUN sed -i 's|<base href="/"|<base href="/blazor/"|' /out/wwwroot/index.html
RUN printf '{"ApiBaseUrl":"/api/v1"}' > /out/wwwroot/appsettings.json

FROM caddy:2-alpine
COPY deploy/oracle/Caddyfile /etc/caddy/Caddyfile
COPY deploy/oracle/landing /srv/landing
COPY --from=js /src/clients/vue-admin/dist /srv/vue
COPY --from=js /src/clients/react-admin/dist /srv/react
COPY --from=js /src/clients/angular-admin/dist/angular-admin/browser /srv/angular
COPY --from=blazor /out/wwwroot /srv/blazor
EXPOSE 80 443
