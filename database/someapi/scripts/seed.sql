CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE SCHEMA funcs;
CREATE SCHEMA tables;

DO
$do$
BEGIN
   IF NOT EXISTS (
      SELECT FROM pg_catalog.pg_roles
      WHERE  rolname = 'executor') THEN
      CREATE ROLE executor LOGIN PASSWORD 'executor';

      ELSE
      ALTER ROLE executor WITH PASSWORD 'executor';
   END IF;
END
$do$;

CREATE TABLE tables.platforms
(
    id uuid PRIMARY KEY,
    title varchar(10)
);

INSERT INTO tables.platforms VALUES (uuid_generate_v4(), 'android'), (uuid_generate_v4(), 'web');

CREATE TABLE tables.users
(
    id uuid PRIMARY KEY,
    email varchar(50) UNIQUE,
    register_date TIMESTAMP DEFAULT NOW(),
    is_email_confirmed boolean DEFAULT false,
    login varchar(20) UNIQUE,
    password varchar(40),
    activation_url uuid
);

CREATE TABLE tables.tokens
(
    id uuid PRIMARY KEY,
    user_id uuid REFERENCES tables.users(id),
    token text,
    expiry_date TIMESTAMP,
    creation_date TIMESTAMP,
    jwt_id text,
    platform_id uuid REFERENCES tables.platforms(id)
);

ALTER TABLE tables.platforms OWNER TO postgres;

ALTER TABLE tables.users OWNER TO postgres;

ALTER TABLE tables.tokens OWNER TO postgres;

CREATE OR REPLACE FUNCTION funcs.register(my_id uuid, my_login text, my_password text, my_email text, my_activate uuid)
RETURNS boolean
AS $$
DECLARE 
    result boolean;
BEGIN
    INSERT INTO tables.users(id, email, login, password, activation_url) 
        VALUES (my_id, my_email, my_login, my_password, my_activate)
    ON CONFLICT DO NOTHING
    RETURNING true INTO result;

    RETURN coalesce(result, false);
END;$$
LANGUAGE plpgsql SECURITY DEFINER;

ALTER FUNCTION funcs.register(my_id uuid, my_login text, my_password text, my_email text, my_activate uuid) OWNER TO postgres;

CREATE OR REPLACE FUNCTION funcs.write_token(my_user_id uuid, my_token text, my_plaform text, my_expiry_date TIMESTAMP, my_creation_date TIMESTAMP, my_jwt_id text)
RETURNS boolean
AS $$
DECLARE
    result boolean;
    founded_platform_id uuid;
    temp_id uuid;
BEGIN
    SELECT id INTO founded_platform_id FROM tables.platforms WHERE title = my_plaform;
    SELECT uuid_generate_v4() INTO temp_id;
    IF coalesce(founded_platform_id, temp_id) = temp_id THEN
        RETURN false;
    END IF;

    DELETE FROM tables.tokens WHERE user_id = my_user_id AND platform_id = founded_platform_id;

    INSERT INTO tables.tokens(id, user_id, token, creation_date, expiry_date, jwt_id, platform_id)
        VALUES (uuid_generate_v4(), my_user_id, my_token, my_creation_date, my_expiry_date, my_jwt_id, founded_platform_id)
    ON CONFLICT DO NOTHING
    RETURNING true INTO result;

    RETURN coalesce(result, false);
END;$$
LANGUAGE plpgsql SECURITY DEFINER;

ALTER FUNCTION funcs.write_token(my_user_id uuid, my_token text, my_plaform text, my_expiry_date TIMESTAMP, my_creation_date TIMESTAMP, my_jwt_id text) OWNER TO postgres;

CREATE OR REPLACE FUNCTION funcs.activate(my_activate uuid)
RETURNS void
AS $$
DECLARE
BEGIN
    UPDATE tables.users SET is_email_confirmed = true WHERE activation_url = my_activate;
END;$$
LANGUAGE plpgsql SECURITY DEFINER;

ALTER FUNCTION funcs.activate(my_activate uuid) OWNER TO postgres;

GRANT CONNECT ON DATABASE identity TO executor;

REVOKE ALL ON ALL TABLES IN SCHEMA public FROM executor;
REVOKE ALL ON ALL TABLES IN SCHEMA tables FROM executor;
REVOKE ALL ON ALL TABLES IN SCHEMA funcs FROM executor;

GRANT SELECT ON tables.users TO executor; 
GRANT SELECT ON tables.tokens TO executor; 

GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA funcs TO executor;

GRANT USAGE ON SCHEMA funcs TO executor;
GRANT USAGE ON SCHEMA tables TO executor;
