/* CREATE DATABASE Remote_compiling ENCODING 'UTF-8' */

Create Table Customer(
	ID serial primary key,
	LdapIdent text not null
);
Create Table Files(
	ID serial primary key,
	LastModified timestamp ,
	Filename text not null,
	Code text not null,
	Stdin text null,
	language text not null,
	fk_user int,
	foreign key (fk_user) references Customer(ID) on delete cascade
);

Create or replace function Files_LastModified()
    Returns TRIGGER
    language plpgsql
    as
$$
BEGIN
    new.LastModified = current_timestamp;
    return new;
end;
$$;

CREATE TRIGGER tr_iu_Files_LastModified
  before insert or update
  ON Files
  FOR EACH ROW
  EXECUTE PROCEDURE Files_LastModified();


insert into Customer (LdapIdent) values ('user:abc');
insert into Files (Filename,Code,fk_user,language) values ('main.c','Hello World!',1,'c');

select * from Files;

Update Files set stdin = 'aa' where ID = 1;

select * from Files;

drop table files cascade;
drop table customer cascade ;
drop trigger if exists tr_iu_Files_LastModified on files cascade;
drop function Files_LastModified;

