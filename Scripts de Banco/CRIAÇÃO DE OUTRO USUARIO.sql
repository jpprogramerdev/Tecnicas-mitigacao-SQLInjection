CREATE USER 'CLIENTE'@'localhost' IDENTIFIED BY  'teste1234';

GRANT SELECT ON CRUD_TG.USUARIOS TO 'CLIENTE'@'localhost';

use user 'CLIENTE'@'localhost'