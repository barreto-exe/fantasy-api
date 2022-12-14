# Contenido
- [Planteamiento del problema](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#planteamiento-del-problema)
- [Metodología](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#metodologia)
- [Requerimientos](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#requerimientos)
- [Procesos de Negocio](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#procesos-de-negocio)
- [Diseño Lógico de la Base de Datos](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#diseño-logico-de-la-base-de-datos)
- [Diseño Físico de la Base de Datos](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#diseño-fisico-de-la-base-de-datos)
- [Diccionario de Datos](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#diccionario-de-datos)
- [Esquema de Seguridad](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#esquema-de-seguridad)
- [Estándares Utilizados](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#estandares-utilizados)
- [Consideraciones realizadas en función de los datos](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#consideraciones-realizadas-en-función-de-los-datos)
- [Esquema de pruebas](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#esquema-de-pruebas)
- [Referencias](https://github.com/BD2-UCAB-GUAYANA/fantasy-api/blob/master/doc/documentacion.md#referencias)

# Planteamiento del problema
Las redes sociales son actualmente una de las aplicaciones más avanzadas y utilizadas, ocupando su lugar a un ritmo vertiginoso y convirtiéndose en una prometedora área de negocio para las empresas y sobre todo como lugar de encuentro. Corrección de errores humanos, así como la capacidad de "interactuar" aunque no los conozcamos, el sistema es abierto y está construido con lo que cada persona involucrada en la red tiene para ofrecer, cada nuevo miembro que se une convierte al grupo en un uno nuevo. Una red deja de ser lo que es si uno de sus miembros ya no forma parte de ella.

Las redes sociales comienzan con la búsqueda de otras personas para compartir nuestros intereses, inquietudes o necesidades, e incluso si no sucede nada más, eso es suficiente. Porque rompe el aislamiento que suele afectar a la gran mayoría de las personas, y que muchas veces se manifiesta en el retraimiento y otros momentos de excesiva vida social sin comprometer los sentimientos.

Las redes sociales en Internet a menudo permiten que la diversidad y las comunidades se unan, y ahí es donde la mayor parte de la energía puede dar vida a los grupos de personas que componen esas redes. El poder de un equipo permite a las personas realizar cambios que de otro modo serían difíciles, creando nuevas relaciones emocionales y comerciales.

Sólo con estas incompletas reflexiones sobre los beneficios psicosociales que brindan las redes sociales a los individuos ,¿queda alguna duda acerca de cuál es la causa del éxito y popularidad que ganan a gran velocidad las redes sociales en Internet?

Dadas solo estas reflexiones incompletas sobre los beneficios psicosociales que las redes sociales ofrecen a las personas, ¿alguien se pregunta qué explica el rápido éxito y la popularidad de las redes sociales en Internet?

Considerando el impacto acelerado de las redes sociales y que son herramientas de alto impacto en la actualidad nos permiten comprender por qué surge la necesidad del desarrollo de un sistema con orientación web al que llamaremos < TOKENZONE | LEAGUEBOOK | o cualquier otro nombre con pegada que propongan los alumnos>, el cual es un sistema que se desarrollará bajo un entorno web con el fin de proporcionar a sus usuarios una herramienta por medio de la cual podrán, compartir, intercambiar, coleccionar y visualizar una serie de barajitas alusivas a los jugadores de fútbol de la Copa del Mundo Qatar, su implementación brindará a los usuarios una opción interactiva y amena de entretenimiento, permitiéndole interactuar con nuevos amigos afiliados al sistema en distintas actividades como trivias, encuestas, subastas, etc.

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

# Procesos de Negocio

# Diseño Lógico de la Base de Datos

# Diseño Físico de la Base de Datos

# Diccionario de Datos

# Esquema de Seguridad

# Estándares Utilizados

# Consideaciones realizadas en función de los datos

# Esquema de pruebas

# Referencias
- [Confirmación de Requerimientos (Offside)](). Presentado por los Estudiantes de la sección 401 de Ingeniería del Software - Semestre 2023-15.
