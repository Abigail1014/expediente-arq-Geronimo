// CLASE 5 · ISP, Interface Segregation (contratos chicos por capacidad) — EL ANTES
// Adaptado al diagrama de Créditos: la interfaz GORDA obliga a cada rol de la financiera
// a fingir habilidades que no tiene (Vendedor, AgenteCobranza, Gerente).

namespace Isp.Antes;

public interface IEmpleadoCredito
{
    void RegistrarVentaCredito(string producto, decimal monto);
    void AprobarCredito(int idCredito);
    void GestionarMora(int idCuota);
    void VerReporteCartera();
}

// El gerente usa TODO: para él la interfaz calza perfecta.
public class Gerente : IEmpleadoCredito
{
    public void RegistrarVentaCredito(string producto, decimal monto) => Console.WriteLine($"[GERENTE] Registra crédito de {producto} por {monto:0.00} Bs");
    public void AprobarCredito(int idCredito) => Console.WriteLine($"[GERENTE] Aprueba crédito #{idCredito}");
    public void GestionarMora(int idCuota) => Console.WriteLine($"[GERENTE] Gestiona mora de cuota #{idCuota}");
    public void VerReporteCartera() => Console.WriteLine("[GERENTE] Reporte de cartera del mes");
}

// El vendedor SOLO registra ventas de crédito... pero el contrato lo obliga a "implementar" todo lo demás.
public class Vendedor : IEmpleadoCredito
{
    public void RegistrarVentaCredito(string producto, decimal monto) => Console.WriteLine($"[VENDEDOR] Registra crédito de {producto} por {monto:0.00} Bs");

    // Métodos-mentira: existen porque el contrato obliga, no porque el rol pueda.
    public void AprobarCredito(int idCredito)
        => throw new NotSupportedException("Un vendedor no aprueba créditos.");

    public void GestionarMora(int idCuota)
        => throw new NotSupportedException("Un vendedor no gestiona mora.");

    public void VerReporteCartera()
        => throw new NotSupportedException("Un vendedor no ve reportes de cartera.");
}

public static class Demo
{
    public static void Correr()
    {
        var vendedor = new Vendedor();
        vendedor.RegistrarVentaCredito("Refrigerador", 3500);

        try
        {
            vendedor.AprobarCredito(101);   // compila perfecto... y revienta en producción
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine($"💥 EXPLOTÓ: {ex.Message}");
        }
        Console.WriteLine("3 de 4 métodos del Vendedor son mentira. La interfaz gorda lo obligó.");
    }
}
