CREATE DATABASE "ConsultaServiceDB"
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

-------------------------------------------------------------------------------------
drop table pacientes;
CREATE TABLE IF NOT EXISTS public.pacientes
(
    id serial NOT NULL, 
    nomecompleto character varying(100) NOT NULL,
    telefone character varying(11),
    datanascimento date NOT NULL,
    datainiciogravidez date NOT NULL,
    email VARCHAR(50),
    cpf character varying(11),
    bairro character varying(40),
    endereco character varying(30),
    numerosus character varying(40),
    CONSTRAINT pacientes_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.pacientes
    OWNER to postgres;

-------------------------------------------------------------------------------------

drop table consultas;
CREATE TABLE IF NOT EXISTS public.consultas
(
    id serial NOT NULL,
    idpaciente integer NOT NULL DEFAULT 0,
    idenfermeiro integer NOT NULL DEFAULT 0,
    descricao character varying(100) COLLATE pg_catalog."default" NOT NULL,
  	dataconsulta date,
  	horaconsulta time,
    idPosto integer NOT NULL,
    observacoes character varying(300) COLLATE pg_catalog."default",
  	situacao varchar(1),
    datasolicitada date,
    horasolicitada time,
    datarealizada date,
    horarealizada time,
    motivocancelamento character varying(150),
    resultados character varying(200),        
    arqresultado character varying(100),
    CONSTRAINT agendamentos_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.consultas
    OWNER to postgres;

-------------------------------------------------------------------------------------

drop table postos;
CREATE TABLE IF NOT EXISTS public.postos
(
    id integer NOT NULL,
    nome character varying(50) COLLATE pg_catalog."default" NOT NULL,    
    CONSTRAINT locais_pkey PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.postos
    OWNER to postgres;

-------------------------------------------------------------------------------------

insert into postos(nome)
values
('UBS Tamarindo'),
('UBS Trezidela'),
('UBS Altamira'),
('UBS Vila Mariano'),
('UBS Centro'),
('UBS Araticum'),
('UBS Vila Alvorad')