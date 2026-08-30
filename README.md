# H1 — Inventario del caso
Sistema: CrediVentas "El Fiado Justo" · Variante 4 · Comercio (venta a crédito de productos)**
Nombre:Abigail Geronimo
Repositorio: expediente-arq-Geronimo
Fecha de entrega: [29/08/2026]


## 1. Actores

Vendedor / Cajero (actor primario) — atiende en el punto de venta. Quiere registrar una venta a crédito rápido, sin trabarse, y poder consultar el cupo disponible o el saldo de un cliente sin tener que llamar al encargado.

Encargado / Administrador (actor primario) — administra el negocio. Quiere ver en todo momento cuánto dinero está fiado (cartera de crédito), enterarse a tiempo cuando un cliente se atrasa, y tener un resumen claro de la cobranza para decidir a quién seguir fiando y a quién no.

Agente de cobranza (actor primario) — da seguimiento a las deudas. Quiere ver de un vistazo quién debe, cuánto y desde cuándo, y no perder de vista las promesas de pago que le hacen los clientes atrasados.

Cliente / Deudor (actor secundario) — no opera el sistema directamente, pero es quien dispara el crédito al comprar fiado y quien recibe la notificación cuando se le acerca o vence una cuota. Quiere que le aprueben el crédito rápido y que le avisen antes de que se le pase la fecha de pago.

Proveedor (actor secundario / externo) — entrega mercadería al negocio. No usa el sistema, pero sus entregas quedan registradas como ingresos de stock que el encargado carga en Inventario.


## 2. Inventario de módulos

| Módulo | Responsabilidad única |
|---|---|
| **Catálogo** | Mantener productos, categorías y precios actualizados. |
| **Créditos** | Gestionar el ciclo de una venta a crédito: evaluación, aprobación y generación del plan de cuotas. |
| **Cobranza** | Registrar el pago de cada cuota, calcular mora y actualizar el saldo pendiente de cada cliente hasta su liquidación. |
| **Inventario** | Registrar y consolidar todo movimiento que afecte el stock (venta a crédito, ingreso, devolución, merma, ajuste). |
| **Notificaciones** | Avisar al cliente cuando se acerca o vence una cuota, y al encargado cuando un producto cruza su stock mínimo. |
| **Reportes** | Generar resúmenes de cartera, morosidad y movimientos de inventario para tomar decisiones. |
| **Usuarios y Roles** | Controlar quién entra al sistema y qué puede hacer según su rol (vendedor, agente de cobranza, administrador). |



## 3. Primer diagrama de clases

Aplicando la receta de 4 pasos sobre el requerimiento de venta a crédito:

**Sustantivos** (candidatos a clase o atributo): producto, categoría, crédito, cuota, pago, cliente, vendedor, agente de cobranza, movimiento de stock, notificación.

**Verbos** (candidatos a método): registrar, aprobar, generar cuotas, calcular mora, pagar, descontar, reponer, avisar, reportar, consultar saldo.

**Filtro** — ¿cuáles de esos sustantivos tienen datos propios y comportamiento propio?
- `Cuota` pasó el filtro (número de cuota, monto, fecha de vencimiento y estado de *esa* cuota específica, no del `Credito` ni del `Cliente`) → es clase, no atributo.
- `Pago` también pasó el filtro (monto pagado, fecha, método, de *ese* abono específico — una `Cuota` puede recibir varios pagos parciales) → es clase, no atributo.
- Tasa de interés y plazo, en cambio, no tienen comportamiento propio → quedan como atributos de `Credito`.

**Relaciones, con multiplicidad solo donde aporta:**

<img width="1200" height="800" alt="image" src="https://github.com/user-attachments/assets/bba07a63-294e-4c56-9a8c-01cdfe3bc4e8" />







## 4. Atributos de calidad críticos
**Idoneidad funcional.** El saldo pendiente que muestra el sistema tiene que ser exactamente el real. En este dominio el saldo de cada cliente lo alimentan tres fuentes distintas (otorgamiento del crédito, pago de cuotas, ajustes o condonaciones); si una sola de esas rutas descuadra el número, el negocio termina cobrándole de más a un cliente honesto o dejando de cobrarle a uno que sí debe. Es el atributo que más plata cuesta si falla.

**Seguridad.** El sistema maneja información sensible del deudor (documento de identidad, score crediticio, historial de pagos), y un acceso indebido puede derivar en fraude, condonaciones no autorizadas de deuda, o filtración de datos personales. Por eso el vendedor solo puede registrar ventas a crédito dentro de un cupo predefinido, y únicamente el administrador puede modificar políticas de crédito o autorizar ajustes de mora.

**Idoneidad funcional.** El saldo pendiente que muestra el sistema tiene que ser exactamente el real. En este dominio el saldo de cada cliente lo alimentan tres fuentes distintas (otorgamiento del crédito, pago de cuotas, ajustes o condonaciones); si una sola de esas rutas descuadra el número, el negocio termina cobrándole de más a un cliente honesto o dejando de cobrarle a uno que sí debe. Es el atributo que más plata cuesta si falla.

**Seguridad.** El sistema maneja información sensible del deudor (documento de identidad, score crediticio, historial de pagos), y un acceso indebido puede derivar en fraude, condonaciones no autorizadas de deuda, o filtración de datos personales. Por eso el vendedor solo puede registrar ventas a crédito dentro de un cupo predefinido, y únicamente el administrador puede modificar políticas de crédito o autorizar ajustes de mora.
