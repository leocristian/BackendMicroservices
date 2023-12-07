

CREATE DATABASE "EnfermeiroServiceDB"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Portuguese_Brazil.1252'
    LC_CTYPE = 'Portuguese_Brazil.1252'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

CREATE TABLE IF NOT EXISTS public.enfermeiros
(
    id serial not null,
    nome_completo character varying(100) not null,
    cpf character varying(11) not null,
    telefone character varying(11),
    coren character varying(10),
    data_nascimento date not null,
    nome_login character varying(20),
    senha character varyingg(100),
    constraint enfermeiros_pkey primary key (id)
)
