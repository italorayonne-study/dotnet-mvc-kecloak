FROM quay.io/keycloak/keycloak:latest as builder


# # Enable health and metrics support
# ENV KC_HEALTH_ENABLED=true
# ENV KC_METRICS_ENABLED=true
# ENV KC_FEATURES=token-exchange,admin-fine-grained-authz

# # Configure a database vendor
# ENV KC_DB=postgres


WORKDIR /opt/keycloak

# for demonstration purposes only, please make sure to use proper certificates in production instead
RUN keytool -genkeypair -storepass password -storetype PKCS12 -keyalg RSA -keysize 2048 -dname "CN=server" -alias server -ext "SAN:c=DNS:localhost,IP:127.0.0.1" -keystore conf/server.keystore
RUN /opt/keycloak/bin/kc.sh build


FROM quay.io/keycloak/keycloak:latest
COPY --from=builder /opt/keycloak/ /opt/keycloak/


# change these values to point to a running postgres instance

# ENV KC_DB=postgres
# ENV KC_DB_URL=jdbc:postgresql://postgres-container:5432/keycloak
# ENV KC_DB_USERNAME=user_vault
# ENV KC_DB_PASSWORD=M41d3nsix00
# ENV KC_HOSTNAME=localhost
# ENV KC_HTTPS_PROTOCOLS=TLSv1.3,TLSv1.2
# # ENV KC_HTTP_PORT=8380
# ENV KC_HOSTNAME_STRICT_HTTPS=false
# ENV KC_HTTP_ENABLED=true
# ENV KC_PROXY=edge
# ENV KC_FEATURES=token-exchange,admin-fine-grained-authz
# ENV PROXY_ADDRESS_FORWARDING=true
# ENV KC_HOSTNAME_STRICT_BACKCHANNEL=true

ENTRYPOINT /opt/keycloak/bin/kc.sh start-dev