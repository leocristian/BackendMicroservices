
CREATE DATABASE "UsuarioDB"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Portuguese_Brazil.1252'
    LC_CTYPE = 'Portuguese_Brazil.1252'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
    
CREATE TABLE IF NOT EXISTS public.usuarios
(
    id serial not null,
    nomecompleto character varying(100) not null,
    cpf character varying(11) not null,
    telefone character varying(11),
    registro character varying(10),
    login character varying(20),
    senha text not null,
  	grupo integer not null default 0,
    constraint enfermeiros_pkey primary key (id)
)