# Readme - ServiMun

Esta aplicación tiene como función, exponer servicios "similares" a los que esperamos que presta la municipalidad.
Esta desarrollada en .NET con la versión 6.0. 
Se trata de una funcionalidad webAPI que expone servicios para:

- Contriuyente --> Servicios CRUD
- TributoMunicipal --> Servicios CRUD
- PadronContribuyente --> Servicios CRUD mas consultas varias
- PadronBoleta --> Servicios CRUD mas consultas mas pago boleta

## Agregado 21/07/2024 - Pago de servicios

Siguiendo el mismo criterio, se agregan servicios "similares" a los que esperamos de proveedor, que nos de acceso a cobrar otros servicios no estatales: Telefonia, Luz, Gas, Television, Colegios, etc.
Los nuevos servicios de la webAPI incluyen las entidades

- Servicio --> Servicios CRUD
- ServicioCliente --> Servicios CRUD
- ServicioBoleta --> Servicios CRUD

