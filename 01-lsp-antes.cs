// CLASE 5 · LSP, Liskov Substitution (el hijo cumple el contrato del padre) — EL ANTES
// El hijo que PROMETE (hereda el contrato) y NO CUMPLE (revienta al usarlo).

namespace Lsp.Antes;

public abstract class MedioDePago
{
    public abstract void Cobrar(decimal monto);
    public abstract void Devolver(decimal monto);   // ← la promesa: TODO medio de pago devuelve
}

public class PagoEfectivo : MedioDePago
{
    public override void Cobrar(decimal monto) => Console.WriteLine($"[EFECTIVO] Cobrados {monto:0.00} Bs");
    public override void Devolver(decimal monto) => Console.WriteLine($"[EFECTIVO] Devueltos {monto:0.00} Bs de caja");
}

public class PagoTarjeta : MedioDePago
{
    public override void Cobrar(decimal monto) => Console.WriteLine($"[TARJETA] Cobrados {monto:0.00} Bs");
    public override void Devolver(decimal monto) => Console.WriteLine($"[TARJETA] Reversión de {monto:0.00} Bs solicitada al banco");
}

public class PagoQr : MedioDePago
{
    public override void Cobrar(decimal monto) => Console.WriteLine($"[QR] Cobrados {monto:0.00} Bs");

    // El proveedor del QR NO soporta devoluciones. El hijo hereda la promesa... y la rompe.
    public override void Devolver(decimal monto)
        => throw new NotSupportedException("El pago por QR no admite devolución.");
}

public static class Demo
{
    public static void Correr()
    {
        // Este código genérico confía en el contrato del PADRE. Y hace bien... hasta que llega QR.
        var pagos = new List<MedioDePago> { new PagoEfectivo(), new PagoTarjeta(), new PagoQr() };

        foreach (var pago in pagos)
        {
            pago.Cobrar(100);
        }

        Console.WriteLine("-- el cliente anula su compra: hay que devolver TODO --");
        foreach (var pago in pagos)
        {
            try
            {
                pago.Devolver(100);
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"💥 EXPLOTÓ: {ex.Message}");
            }
        }
        Console.WriteLine("El tipo dijo 'soy un MedioDePago' pero no se comporta como tal: LSP roto.");
    }
}
