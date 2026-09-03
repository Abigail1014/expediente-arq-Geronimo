// CLASE 5 · LSP, Liskov Substitution (el hijo cumple el contrato del padre) — EL DESPUÉS
// Contrato honesto: el padre solo promete lo que TODOS los hijos pueden cumplir.
// Lo extra (devolver) vive en un contrato aparte, solo para quienes SÍ pueden.

namespace Lsp.Despues;

public abstract class MedioDePago
{
    public abstract void Cobrar(decimal monto);     // ← lo ÚNICO que todos cumplen
}

public interface IReembolsable
{
    void Devolver(decimal monto);                   // ← contrato aparte, opcional
}

public class PagoEfectivo : MedioDePago, IReembolsable
{
    public override void Cobrar(decimal monto) => Console.WriteLine($"[EFECTIVO] Cobrados {monto:0.00} Bs");
    public void Devolver(decimal monto) => Console.WriteLine($"[EFECTIVO] Devueltos {monto:0.00} Bs de caja");
}

public class PagoTarjeta : MedioDePago, IReembolsable
{
    public override void Cobrar(decimal monto) => Console.WriteLine($"[TARJETA] Cobrados {monto:0.00} Bs");
    public void Devolver(decimal monto) => Console.WriteLine($"[TARJETA] Reversión de {monto:0.00} Bs solicitada al banco");
}

public class PagoQr : MedioDePago
{
    public override void Cobrar(decimal monto) => Console.WriteLine($"[QR] Cobrados {monto:0.00} Bs");
    // No firma IReembolsable: no promete lo que no puede cumplir. Honestidad de tipos.
}

public static class Demo
{
    public static void Correr()
    {
        var pagos = new List<MedioDePago> { new PagoEfectivo(), new PagoTarjeta(), new PagoQr() };

        foreach (var pago in pagos)
        {
            pago.Cobrar(100);
        }

        Console.WriteLine("-- el cliente anula su compra: se devuelve donde SE PUEDE --");
        foreach (var pago in pagos)
        {
            if (pago is IReembolsable reembolsable)
            {
                reembolsable.Devolver(100);
            }
            else
            {
                Console.WriteLine($"[{pago.GetType().Name}] No admite devolución: se emite nota de crédito");
            }
        }
        Console.WriteLine("Cero explosiones: cada tipo promete SOLO lo que cumple.");
    }
}
