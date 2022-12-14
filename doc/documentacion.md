# Contenido
- [Planteamiento del problema](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#planteamiento-del-problema)
- [Metodología](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#metodolog%C3%ADa)
- [Requerimientos](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#requerimientos)
- [Procesos de Negocio](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#procesos-de-negocio)
- [Diseño Lógico de la Base de Datos](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#dise%C3%B1o-l%C3%B3gico-de-la-base-de-datos)
- [Diseño Físico de la Base de Datos](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#dise%C3%B1o-f%C3%ADsico-de-la-base-de-datos)
- [Diccionario de Datos](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#diccionario-de-datos)
- [Esquema de Seguridad](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#esquema-de-seguridad)
- [Estándares Utilizados](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#est%C3%A1ndares-utilizados)
- [Consideraciones realizadas en función de los datos](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#consideraciones-realizadas-en-funci%C3%B3n-de-los-datos)
- [Esquema de pruebas](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#esquema-de-pruebas)
- [Referencias](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#referencias)

# Planteamiento del problema
Las redes sociales son actualmente una de las aplicaciones más avanzadas y utilizadas, ocupando su lugar a un ritmo vertiginoso y convirtiéndose en una prometedora área de negocio para las empresas y sobre todo como lugar de encuentro. Corrección de errores humanos, así como la capacidad de "interactuar" aunque no los conozcamos, el sistema es abierto y está construido con lo que cada persona involucrada en la red tiene para ofrecer, cada nuevo miembro que se une convierte al grupo en un uno nuevo. Una red deja de ser lo que es si uno de sus miembros ya no forma parte de ella.

Las redes sociales comienzan con la búsqueda de otras personas para compartir nuestros intereses, inquietudes o necesidades, e incluso si no sucede nada más, eso es suficiente. Porque rompe el aislamiento que suele afectar a la gran mayoría de las personas, y que muchas veces se manifiesta en el retraimiento y otros momentos de excesiva vida social sin comprometer los sentimientos.

Las redes sociales en Internet a menudo permiten que la diversidad y las comunidades se unan, y ahí es donde la mayor parte de la energía puede dar vida a los grupos de personas que componen esas redes. El poder de un equipo permite a las personas realizar cambios que de otro modo serían difíciles, creando nuevas relaciones emocionales y comerciales.

Sólo con estas incompletas reflexiones sobre los beneficios psicosociales que brindan las redes sociales a los individuos ,¿queda alguna duda acerca de cuál es la causa del éxito y popularidad que ganan a gran velocidad las redes sociales en Internet?

Dadas solo estas reflexiones incompletas sobre los beneficios psicosociales que las redes sociales ofrecen a las personas, ¿alguien se pregunta qué explica el rápido éxito y la popularidad de las redes sociales en Internet?

Considerando el impacto acelerado de las redes sociales y que son herramientas de alto impacto en la actualidad nos permiten comprender por qué surge la necesidad del desarrollo de un sistema con orientación web llamado **Offside**, el cual es un sistema que se desarrollará bajo un entorno web con el fin de proporcionar a sus usuarios una herramienta por medio de la cual podrán, compartir, intercambiar, coleccionar y visualizar una serie de barajitas alusivas a los jugadores de fútbol de la Copa del Mundo Qatar, su implementación brindará a los usuarios una opción interactiva y amena de entretenimiento, permitiéndole interactuar con nuevos amigos afiliados al sistema en distintas actividades como trivias, encuestas, subastas, etc.

Además, permitirá solventar las deficiencias a la hora de compartir información, facilitando el contacto entre un grupo elevado de personas por medio de redes de amistades el cual podrá ir incrementándose a deseo o conveniencia.

# Metodología
Reconociendo a **SCRUM** como un marco de trabajo para el desarrollo ágil, lo escogimos en lugar de una metodología. Para las necesidades de nuestro proyecto, se determinó que SCRUM era el modelo más indicado,principalmente por su amplia flexibilidad, que permite manejar prioridades cambiantes a medida que avanza el proyecto, posibilitando la resolución de las incertidumbres que se puedan presentar.

A su vez, permite trabajar con mayor agilidad, debido a la manera en la que se gestiona el tiempo (intervalos de tiempo), que, también, permite segmentar el proyecto en pequeñas partes con objetivos alcanzables donde además los integrantes trabajan en un ambiente abierto, donde se conocen los procesos y desafíos que enfrentan cada uno. Finalmente, debido a su modelo, este permite el uso de herramientas como tableros de tareas, reuniones, revisiones, asignación de recursos, entre otros, que permiten reducir el riesgo de fallo.

## SCRUM Daily
Consiste en una reunión que generalmente tiene una duración de 15 minutos, se realizan diariamente las tareas y se explica en lo que se está trabajando. También, da visibilidad a todo el equipo sobre el estado general del proyecto y busca solucionar bloqueos de una forma rápida. Nuestra implementación de esta reunión en un principio será hacerla de forma asíncrona por medio de mensajes por chats de equipo.

## Sprint Planning
Define objetivos para el siguiente sprint, selecciona tareas del bando de producto, y se deciden las tareas que pueden entrar o no dentro del sprint. Es necesario ser realista y calcular cuánto tiempo va a llevar.

## Sprint Review
El equipo analiza el trabajo realizado, las tareas, calidad, se hace una demo a los clientes y personas interesadas, ofrecen visibilidad al estado del trabajo. También informa de lo que se ha podido o no se ha podido hacer.

## Sprint Retrospective
Analiza la forma de trabajar, busca mejorar el flujo de trabajo del equipo, analizando problemas y bloqueos del Sprint anterior. Buscando soluciones para las mismas.

## Scrum de Scrums
Debido a la cantidad de personas que están involucradas en el proyecto, es necesario escalar la metodología original, por lo tanto, se optó por el uso de Scrum de Scrums, que consiste en segmentar los grupos de trabajo en otros más pequeños, pudiendo así coordinar equipos independientes, que garanticen un producto integrado al final de cada sprint. En este caso, todos los equipos realizan las mismas prácticas, y se genera un nuevo rol llamado “Experto en scrum de scrums”.

## ¿Cómo organizar un daily sprint?
El Daily Sprint tiene como objetivo informar a todo el equipo sobre el estado del proyecto. Permite manifestar que todos los miembros se pueden ayudar entre sí. Cada miembro revisa el trabajo del resto, para que al finalizar la reunión, se puedan hacer las adaptaciones necesarias para cumplir con la previsión de objetivos. Debe ser divertida y ligera pero a su vez ser informativa donde cada miembro debe responder a las siguientes preguntas:
1. ¿Qué hice ayer?
2. ¿En qué trabajaré hoy?
3. ¿Estoy siendo bloqueado por algo?
Se tiene que informar lo que se haya completado el día anterior frente a todos los compañeros. Ya que nadie desea trabajar con alguien que siempre hace lo mismo, a su vez, el tiempo y claridad de la reunión es importante para el éxito de la misma.

# Requerimientos
Los requerimientos del sistema, tanto funcionales y no funcionales, fueron definidos en lenguaje natural en forma de historias de usuario con el fin de lograr un manejo más sencillo y práctico para todos los stakeholders. Los mismos quedan planteados a continuación.

## Obtener cromos

- Abrir sobre diario
    - Yo como usuario quiero abrir un sobre diario para completar el álbum.
- Obtener recompensas (cromos)

## Álbum I

- Ver álbum
    - Yo como usuario quiero ver mi álbum para observar las páginas que he completado.
    - Yo como usuario quiero ver el álbum por páginas de cada equipo para saber los cromos que pertenecen a él.
- Ver carrusel de cromos disponibles a pegar
- Pegar cromo al álbum

## Auth I

- Login
    - Yo como usuario quiero loguearme en la plataforma para hacer uso de ella.
- Registro
    - Yo como usuario quiero registrarme en la plataforma para disfrutar de sus servicios.

## Auth II

- Recuperar contraseña
    - Yo como usuario quiero que el sistema me envíe un correo con un código secreto para recuperar mi contraseña.
    - Yo como usuario quiero que validen el código secreto que me envió el sistema para recuperar mi contraseña.
- El jugador puede escoger equipo favorito

## Ver anuncios

- Ver anuncio de popup en el sobre diario

## Gestionar usuarios

- Crear usuarios
    - Yo como administrador quiero crear a los usuarios.
    - Yo como administrador quiero asignar roles importantes dentro de la plataforma para controlar a qué tienen acceso los usuarios.
- Listar a los usuarios
    - Yo como administrador quiero listar los usuarios.
- Editar información de los usuarios
    - Yo como administrador quiero editar la información del usuario.
- Eliminar usuario
    - Yo como administrador quiero eliminar usuarios.

## Gestionar cromos

- Crear cromo
    - Yo como administrador quiero crear cromos para añadir nuevos cromos al álbum.
    - Yo como administrador quiero crear un cromo para los usuarios.
- Listar cromo
    - Yo como administrador quiero listar cromos para observar los cromos que componen al álbum.
- Editar cromo
    - Yo como administrador quiero editar cromos para modificar los cromos que componen al álbum.
- Eliminar cromo
    - Yo como administrador quiero eliminar cromos para quitar cromos que componen al álbum.
- Ajustar probabilidad de salida del cromo
    - Yo como administrador quiero ajustar la probabilidad de que salga un determinado cromo para tener cromos de distintas rarezas.
- Cambiar el equipo de un cromo
    - Yo como administrador quiero establecer el equipo de un cromo.

## Gestionar equipos

- Crear equipos
    - Yo como administrador quiero crear un equipo para agrupar a los jugadores.
- Listar equipos
    - Yo como administrador quiero listar a los equipos para ver a equipos y jugadores que conforman el equipo.
- Editar equipos
    - Yo como administrador quiero editar los equipos para cambiar la información del equipo.
- Eliminar equipos
    - Yo como administrador quiero eliminar equipos para mantener la información del álbum al día con respecto a los jugadores.

## Gestionar anuncios I

- Crear anuncios
    - Yo como administrador quiero crear anuncios para publicitar a mis anunciantes en la plataforma.
- Listar anuncios
    - Yo como administrador quiero listar anuncios para saber los anuncios que tengo activos en la plataforma.
- Editar anuncios
    - Yo como administrador quiero editar anuncios para modificar los anuncios de mis anunciantes en la plataforma.
- Eliminar anuncios
    - Yo como administrador quiero eliminar anuncios para quitarlos de la plataforma.
- Exportar reporte de estadísticas de los anuncios

## Mercado

- Ver subastas
- Ver subastas que yo inicié
- Ver subastas en las que participó
- Crear subasta
    - Yo como usuario quiero realizar una subasta para ganar monedas.
    - Yo como usuario quiero obtener el dinero de mi subasta.
- Participar en subasta
    - Yo como usuario quiero participar en una subasta para obtener otro jugador para mi plantilla.
    - Yo como usuario quiero recibir el jugador de subasta en caso de haberla ganado.

## Banca de jugadores

- Obtener jugadores
- Ver banca
    - Yo como usuario quiero tener a los jugadores que no están en mi equipo en la banca para saber qué opciones de cambio tengo.

## Plantilla de jugadores

- Gestionar jugadores del equipo
    - Yo como usuario quiero agregar jugadores al equipo para armar mi equipo ideal.
    - Yo como usuario quiero quitar jugadores del equipo para adaptar el equipo a mi equipo ideal.
- Mostrar últimos puntajes de cada jugador
    - Yo como usuario quiero ver los últimos puntajes obtenidos por los jugadores de mi plantilla en sus respectivos partidos más recientes para poder ver cuántos puntos ganaron/perdieron.

## Distribuir recompensas

- Tarea programada para distribuir recompensas
- Distribuir puntaje a cada usuario dependiendo del rendimiento en el partido jugado

## Ranking global

- Dar rango a los usuarios por su desempeño en el fantasy

## Dinero del fantasy

- Recibir dinero inicial al inscribirse en una liga
- Ver tu dinero

## Gestionar partidos

- Cargar información en “Gestión de resultados”
- Modificar información en “Gestión de resultados”
- Listar información en “Gestión de resultados”
- Eliminar información en “Gestión de resultados”

# Procesos de Negocio
A continuación, se plantean a manera de diagramas las diferentes actividades, procesos y tareas que se realizan dentro del sistema para lograr satisfacer los requerimientos del negocio. Se presentan en mapas de procesos y diagramas de flujo de datos.

## Mapas de procesos
![Usuario](https://lh4.googleusercontent.com/rMH7OcGEPMDX4gyq6V_XcPd1O1ssS2VrEWthB2qOebrVnuf7yh63xjacpZL3UAdi7TH2KunkzxCjay1TS2tJoYlYdui11admDdTkY24aBuXSDqQhjYeckrtyIYSo8XdqKdk6e5c3BHF-ic1mdVuAj9mQJ25vPwtGM3ulXS8xX62lNxg2tPDX_Cas8hX_7g)
![Usuario2](https://lh4.googleusercontent.com/gfCNPb0sMDNY5Nt5MM9U7uoW6KpErWncN3zhikyzHBTb_YQ-N5MAcQieFuMJsxL5cJLkzdokA1Lt7VLeEqW4QaDudxDinnODsROCZFWaRS9eDOw0wOWUKbHAi9lPETK1RhvtQ9UeY5rcQZkuQUUiwLEMq3-SqKsT-2d_nMN8Fj0lqJkZaOV2BDMltvd98g)
![Usuario3](https://lh4.googleusercontent.com/gfCNPb0sMDNY5Nt5MM9U7uoW6KpErWncN3zhikyzHBTb_YQ-N5MAcQieFuMJsxL5cJLkzdokA1Lt7VLeEqW4QaDudxDinnODsROCZFWaRS9eDOw0wOWUKbHAi9lPETK1RhvtQ9UeY5rcQZkuQUUiwLEMq3-SqKsT-2d_nMN8Fj0lqJkZaOV2BDMltvd98g)
![Usuario4](https://lh4.googleusercontent.com/Ax4eU4EWo3cN024fqYHR6BZjlgntyh1XiSYPLUS_OydpaOoHXLR3sEhAkGSwt-T459TnyXzMKVyEpJHDykIDK8pRH_FdkK5B3O3_biOqcEmYiaRlBJFmLVxO7cPwlMxYbZbaNC5AOOTcaaDD2UHVlOmUAlJnKLEmCkw4gZc8_msAlbF8hhwlPK7W40Tymg)
![Admin](https://lh4.googleusercontent.com/tPfQRxZVEac7nlbJjnGzj-IndHkGvqNJxk8Zb2YLb_HB-Qc0ogfLQXlKAG-TF8PY8npZ7SmjVKP14Q0FlCsJ598HNktuP_9ALid3zyIZNFcp61XZ6d0NeDcCNjacw7tYah24MINXeZa3saBbEazl9ySKh65Ts0xn0SkPL3pwhNvjbx2R9LzU8hAOzew59A)
![Admin2](https://lh6.googleusercontent.com/9c74SEII0Tm4Y85vzrBnEsFsEi8_O7VasRd0EzPnf0n_QoiF9_aIEna4kzwn4xf0b-1UWD-vuQfki6G6_EqFmIrO-1C8zO---a4e6lQKS7JTzuqiLwB6oclbJDN2zEA8YzgDW9w8VPuo-kvUtmVMrNkUYY0umJvcxd9b8IrXtN-9y7mfPVKvOwumzxloxQ)
![Ads](https://lh3.googleusercontent.com/cyHCBVBJnyuHiA3rWzIego1mVbBatiuCscKFKScCeGbpuWGH6Spx4a46vjglJx9LdVjZjgIVQa0xxDC7wFhbZQzL63fPkJN0pgI3AfFR4ZOPuE-knJEOLYx_7IG1ADemwBR4ST50aIgqO_Edx-F5RS4Jheo-RJX-WYw88BSRkKGr1xFNF9bZwDVdQbryFQ)

## Diagramas de flujo de datos
![Nivel 0](https://lh6.googleusercontent.com/S327yGurgURMipM4xCVJTbDiSm4tlPRuvRWhDBnjAAzC7RVBF6dSCdwp0J9Hk9sqys6lG69z-lbo0du1M5npAvCT9Im7Ndr1TFzkF0w0JE0TQjVU2yOGnJjRQTtunENsUB0JGseRSkoMnqHJRghJUdEBZIlMceGmxKXpDJZgI4-UQJgMUUsVc9DupWMVew)
![](https://lh6.googleusercontent.com/klMSMQ2ucjjB347tZc6xMGCU98hQ9A6DGExkWce-Cmbvwi502qX4G_8pSqP_nLESy0QM-SSrKQZb5sdFa_rIw7tMJE-6GV9GYbCf4bTYQly-jEJElOOqtxQkwGNi2OJgg0Srqdkjyes9tZRSEScceb7iB5LtjNB13noUK6Kt88ix1vJ4Rk4ScN62dma6gQ)
![](https://lh5.googleusercontent.com/HpEsPpmD3ghBZMauWd98vBJXTQHzhP4GguRZc8baNJg4ebuvqgZ8VEFgWg0Ja4P6lGT2Z9DJJqB9-AfVjA30DJMyuJZ0sRn9BQpmqGjkETgT-a6ZYdbHbBYfUHMR8nNcOgNFd_60d3XCWr4ko78ioxzAbaYdBamxefSG3NHNizNCpTZ6EGlhbpaZ-uvjuQ)
![](https://lh5.googleusercontent.com/bgZu_rqeEdfG6fGSOoa34lH1dKyc5sy8KzE45n1GKfaR6SFgI0OvQ29gaBG3o3DDleaUPna3Y_U988a7gkqNEIZI7QehWkhUgLwr7YdVKxX3PLOFDsficdcJiQ9gutGJ4BHwRhwfbxLQMyFQGoatHQ2YAq9KbVHk0BVIY41IyLAE5p6rAT8fWRayRBJYTA)
![](https://lh3.googleusercontent.com/haKZYx3M2onRmR6-k3zreucMK4SYKCqIUc6egwnzamAOrBW5DH6lGCVkTtvhTBNXCU0Y1iRW44pRjYc4ev9ByrhCrb07DS3JwADkavyz3LQkCnsXS2PY-TPXdMTLhpETi5hJE7n6sCCBEH46NhvMACanW4AQUpvSxzy_FrUR192WM8_FG5LbhmNv_Zt45A)
![](https://lh5.googleusercontent.com/sagrnW8CnYuBnw6N72Bqz7v62rsyxUGy_fwTgREe0gn3y31-vrnFv0AxjqdQYUG-fPnHZq3OBRL1H166ZSndX-UDFfBlTuaKKeYjuT-GCW4ylPf7MB6yyd9uV2Tz1TrCOB5f-nuRAM_HoXof-lxTCtWBB2vH732OjXyhcaQVVI6AWe1wS3w6AqW-H0VzuA)
![](https://lh6.googleusercontent.com/xKy7JAU8iL4NhYXDoLAM8ldjyPTpphfam4DYil_OHtSLzuQ82VsZ-bTbvMdd0zH23qmKElx0YLebqQssKsAfn8HTc370IzK4DYL3qmh1LjUGhS1AiVNBTAB7ngFlWmJReV41btH5hssFBO5MAVv4HInjptUClISVEeZppnMX1glsZpUQ3WUIJe1tqfYX1g)
![](https://lh3.googleusercontent.com/Pn0AgeHfLiV7CvkgrLNsGc_ccLclX0NJfljO_ONLLIDSiHwPbKI_ZRiXdMRTIA6rE4I79kzE552GL5dq1_kgTbgO717UKeQe8XU9KMPz2zijSR0qRsosE4BWP-zl8nIBlStnQsqLPJvYXjRTEcTd3qHdMA95_KI_hWexEqAQxrCADrS-zKEBFuxf6V_UrA)
![](https://lh3.googleusercontent.com/OncpvnfmG4WpksLbJv2q-_BWSj8d9M7Y9F-GWM9EBa0lx4F2wH3FREqnJO5f1u_0ok-MYVzwtXsk-mpTRZ2U8L5uukqANt-7DzGKJO6QQBL2llx7u-Fll7VyCi9ZrQE_69PpNIanpEJr8-zhQdGt3O0j-ePcDDgIzVSUZYHMyzAyTCw7mAS6U2Lb6HVFZA)
![](https://lh5.googleusercontent.com/xLF-81QYGzolUCDu5t-HWZGBrUCksGRm2s-pt2dEgmQJalhrNF_RmX15DvjujRdgn5HjjOZ2bzXFm0oR3MTwROvisDuZ0tUB77g58rkvzKraUArPI367x4LQewE-hlh7m3zAmqgj-KYs8dC1oz2ZTB3Nxjc80w63nmVy_uo0qhYCXWMrZ6vQBIXKp70RwQ)
![](https://lh3.googleusercontent.com/px43kvsoz9_8VgndYP3gya3Gwv3PMz2lJDdvCWbwjccjILOu2Cl_qq_9hBxI9kwxX2pKVdDjUXyEedu5uyZMVfPxkeqHLO0qKjObQjLPyQ8jSs8yyEGZ2y9pYpGVu3_tm1PLC9e0KU6a1qrnWFFHHzdiPAddB4wiQmU6CsJGU4OJOfMiYjc-JfVrVU7ATg)
![](https://lh4.googleusercontent.com/JecoTAlqIuD9cPHm_qzQM7kCZR_qbUo-NDywq7qgFAsoa-4A14i738YSbX3zeYrVnQrtS8tD_mbJy6D8paJ0XI_eibHw2F_78QfV2zbg2f_y4dAuh1DZ3y4-vRjwO4JmptE7p2NCslZcqJpgx8m2y-Qkm_-f0d38jV0ueVzGcz7NrW1uxCqNSmPs_N08Vg)
![](https://lh4.googleusercontent.com/N622eVLiM12PoPvEnZ-r-NEJxbepdTYoloYDbmT1uDBDqYtFeMlSOqRG6QxPBjwF4INRKsmS8XBMm-w-S8wG7Xua-N-el2NW1rL_j4fDVyC105kWXV-qppsvv9hQJkUTrRkDprfeL4MDBQvZLpIQVLW1-OqEWRpJpI8X1g3x0f3G4KYA-b5caUMU59Rghg)
![](https://lh3.googleusercontent.com/DmR_gLdQbic4I5Tglf5lNdl-AhHmFpOLYCgBzPViT2cKKHXSY9MZpY9OUPcYMFGwKDdsV8ZGT91uzbbESdeUgThCxHmUKohJa5Iybiq3fVjP6L_ujXSFQZCCaoVnyB7xfzpAeqX4k9CuHCCmRDulx8kqe-SD8ePylBMvN6g07QTjEYw_0UtaQEcZ8C0sQA)
![](https://lh4.googleusercontent.com/Coakp0M1Gz9j_psr6ni--Hpg2dbFDleiN5vjCSHZBzjrWG2gf6JBB3kSEh1mA77vZfAAo_uCcNWXpOEc4ajv3Tc-1limefTeen1ThOaODnBTOF2A2xUoKHN6sONbUH72wF1Px1fJK_91K7G3uU0zkoSqtMcruwZmuuYfizwOvLq6r9kEfc4zyJa37i4-0w)
![](https://lh5.googleusercontent.com/dUnI0DYdpIuVG15oYf9WKWf2Ukpoef-RsKTkr1D8uNPo5AfRB0mg5Kk5knUjpGlZV3t1LvDaCzBtPQA7krcJi29RC1kQm5orHy2mDYOVZn3S6miu0AIM-yi2CKNhqCd8X5ia0AOkYnAAP3dcxsMoLDWfqAivTsOIqPq11gn2B-Duu4WV2mW7Kt3yHImBCA)
![](https://lh5.googleusercontent.com/IZ15n7iPzwSnHMHvFk6XeMCBw8ua6XhFL88i3fA7249b1bi7G76V_V6vW8ESno5e8qTHduo5qkOV8ljBLrripIzWeZPamFzuu0PfEEaq8D_qxE4V-Qhy4e4Wt-XHeB7AisKMDzsZTfpHROCDzS4DasA-_otxRwVL-2nfQX_3D0Ot2ypO9nBAnMHra-PGVg)
![](https://lh4.googleusercontent.com/Z2SlRrPdenVjZaSfXsO3-okwNCaB1rB6HVpbIJT4CJACrzvTxX3hn5mC0QmU85hzK5UXArfdmEPIeVyYj9PKkklwnlSMKRxAFp6LlcUSR9zZYdRvg1sJBiFEndSjg6-sMqP87VqSkEtKMZnrNWxpblvnO-rUm6d95NQ-k1V6LVuL3fg-T-J9Ksq0XPG5RQ)
![](https://lh4.googleusercontent.com/ISVik30W5A9b-IfVXm26ECLo2xVeZD2gAdsvl6clSCRgBypiFgJsczJYcy6Oen_f40KzLsFnFX9mlVln7TxwULL80fTR8GHSU_fpObyuDSzELP-SLrwrsTeS1P1317XDEscweBMZsx0NY-69zIT288OWA7O3SwQOB6UI6x-GjFOF4Y1jITnTTcqNmN1iGg)
![](https://lh3.googleusercontent.com/sGVOhvk6mApQDnVIvsM20ZI-Txp2WunJ699xBvsVFmXFwkV8NOM9-6TTinM2yDMXlhnVYGzbbWc7u9RV1pPqYxFxvNVvVDBggCJcuzoIVfYakduLwdZIJlxBrNIOYpnWd_SBGBIDJyJ-onJw425cmW2uo9FsgEmNlCAjFuI55xi-37EHoGk3ss91jDqBGQ)
![](https://lh4.googleusercontent.com/B9Ob7TRmmmuiv9DhBHBDdPgBU2YdSNPipZdWBIRLJZlPHHUdf_gugl1Gwu5Qu1i1RpgY95qauZrAOVBXz05jfwU2jprAOS0G3idez2FtZD3mIAmu6hytKEEOAAoE_B7I0_yKOd7JViWCdNSA-4s5_mZoAVZMhK8KHqxrKtITDW55DXbSylaPOjm8W3-wpg)
![](https://lh3.googleusercontent.com/0PZpoFx0SZdi7qJDrY6s5sdYYk4LZ8lm6JzOY_2wWmVz92ECj2xMj0JkpMyk7-70vvlrS5AZTtEhK6UtV5AKi4BCIStluSczPFms74ND2OaESg8QaNWyj-7z8-RmdV3snz2mkfPXJdoXswAWl-LVZciql37V9VvX5Pul4uw9do_Q6vO_o0CxzgyTbEmqbw)
![](https://lh6.googleusercontent.com/gmfEqWV7hp4kxmKWkXZ823SQKxoGk4vWDfQUtTxybNC-fPHd6kWHDecKV6ljqZPjrbp-jOrFVL6XCtbRCCuI6rI5ZIg7k_cFR1iCXe5LbBDFl9IjVhld2FATbvglAC77AGGdeMoSi-aNuKJ_MEDzXuD5Q8YpkS0Q5R-SJmOS2bCvO4tTsFxvOQTTEXfBEg)
![](https://lh6.googleusercontent.com/QIbRTcfuOGu-oLtWK79Mupua7ZpAxgdOeh4xIYVntazW5DiUiaAr3V82V-4F_s501POc4ETOmPaQMkExypgPK4buluknpQT5g6ZaMhzCb-yrfuEI9pSD5uBV4GsjSuiXptR5y9HtVDuNULkAD0cNfXGrefcXjJerQfqD6uIR8Ky1CdrgDQ3V4Sxw8_4gmA)
![](https://lh4.googleusercontent.com/mLulBOmvC15hPvOkMI5znNSSShtlnq59E69wV0wYKx0mcyp37G3AVTd5PPZqxG2Zvo9KttbUvDii3zkYf4ja83cizVd0-tlg_BFj4t9XeuKEL5EA_-TnoVCVGPgxsZnCb8dImOBv83pzB-enFNtRuT_XTzY1vnGAzZuA-z_Vt2u9T123EGohp_CWntdkyg)
![](https://lh3.googleusercontent.com/bLOM5HOVCTgv-nqD5TvSlLH8s8NwEemANCoqc0k3naekymGK4snrqG2GYIasvt-oxcMTQNH0MTBsC8SH9ynVkAcrSxIL8zBvU9DQuu1bYGgwB3J_GBSr0FCcpV6eMu7fm-bHHemizHaDVi81XnDuVbi_ibHVqbHm_IsDu6D5uSD8UkyhLx6g_VBWOA8_7w)
![](https://lh3.googleusercontent.com/hXc_okC7aJSJQHtS78aQXuFrpQE_9xV8P7xYh7IPJO3nsh6gL1gKidtmewTUMJ5TJh7oao9NzS5jBkBs5fHZciiyq3eQ4pWoGgM50pCkuqStNnZhJI3pSpmjMgjsw01Yy-EDvYaAF36Tq_jFqKx6xItyJOBvMT7RMHq82nPBAOF-1qFxbrqb8-OranTVGw)
![](https://lh4.googleusercontent.com/iPvDZ3flCyRhlLvX3VoZ09y8MwcRj40OYk_Zv7iw_X68bH9NIM7apRDju-_V7NSsBObk2u01wulzvlfx8M1kJ25J7FmNcLECW3KHTGz1P-Mk6GH9kTwlKihpuURpsgzt3TgerqaYF_grZv0toYn3pE0ie2fvuxfU15KwlTENGK9It-haBdNzHQ45wwIzZA)
![](https://lh4.googleusercontent.com/Xn5lke5Pv58P3QxO_zo45y_jn8HjHMnvd3qlMo_yUbVK_QsyzIWINOVLXlOyDhffT8obNDfM-h3TzXRJrFaI1CHK2kGzfDk9yHzloBg3DDnOS-0EgOpNS7HHcDA5WZc49FgsuTzFfVb2-bPjM_LLnpqebjsVM2KFl6Ih8rjT3jIqYS2675DxPFyLNxtIfg)
![](https://lh3.googleusercontent.com/koI0RMkviB_P-oGzTqdJVHWj_D3N7ERJNh5Ke_OvafUSxgle_RUDBBci5DpkmOMOIJW7BFTkaqGa6qve8KPLz9JFlffYMGwEK7-IkkOcLeN6pCFdNK3IWSq7XBrmTlensSkuxjD7peLtibjee3QGydwYN9miAwUvZyPnY6DbokDYs9S2Wfc7slZmu-oACQ)

# Diseño Lógico de la Base de Datos

# Diseño Físico de la Base de Datos

# Diccionario de Datos

# Esquema de Seguridad

# Estándares Utilizados

# Consideraciones realizadas en función de los datos

# Esquema de pruebas

# Referencias
- [Confirmación de Requerimientos (Offside)](https://drive.google.com/file/d/13Kc3CiV1jfGLhp6-XI89TrVH_v-uWUe7/view). Presentado por los Estudiantes de la sección 401 de Ingeniería del Software - Semestre 2023-15.
