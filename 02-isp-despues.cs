// CLASE 5 · ISP, Interface Segregation (contratos chicos por capacidad) — EL DESPUÉS
// Contratos chicos por capacidad: cada rol de la financiera firma SOLO lo que sabe hacer.

namespace Isp.Despues;

public interface IVendedor
{
    void RegistrarVentaCredito(string producto, decimal monto);
}

public interface IAnalistaCredito
{
    void AprobarCredito(int idCredito);
}

public interface IAgenteCobranza
{
    void GestionarMora(int idCuota);
}

public interface ISupervisorCartera
{
    void VerReporteCartera();
}

// El vendedor firma UN contrato: el que cumple. Ni un método de mentira.
public class Vendedor : IVendedor
{
    public void RegistrarVentaCredito(string producto, decimal monto) => Console.WriteLine($"[VENDEDOR] Registra crédito de {producto} por {monto:0.00} Bs");
}

// El agente de cobranza firma solo lo suyo.
public class AgenteCobranza : IAgenteCobranza
{
    public void GestionarMora(int idCuota) => Console.WriteLine($"[COBRANZA] Gestiona mora de cuota #{idCuota}");
}

// El gerente firma los cuatro contratos: para él nada cambió.
public class Gerente : IVendedor, IAnalistaCredito, IAgenteCobranza, ISupervisorCartera
{
    public void RegistrarVentaCredito(string producto, decimal monto) => Console.WriteLine($"[GERENTE] Registra crédito de {producto} por {monto:0.00} Bs");
    public void AprobarCredito(int idCredito) => Console.WriteLine($"[GERENTE] Aprueba crédito #{idCredito}");
    public void GestionarMora(int idCuota) => Console.WriteLine($"[GERENTE] Gestiona mora de cuota #{idCuota}");
    public void VerReporteCartera() => Console.WriteLine("[GERENTE] Reporte de cartera del mes");
}

public static class Demo
{
    public static void Correr()
    {
        var vendedor = new Vendedor();
        var cobranza = new AgenteCobranza();
        var gerente = new Gerente();

        vendedor.RegistrarVentaCredito("Refrigerador", 3500);
        cobranza.GestionarMora(101);
        gerente.VerReporteCartera();

        // vendedor.AprobarCredito(...)  ← ya NI COMPILA: el error se atrapa en diseño, no en producción.
        Console.WriteLine("El error imposible: lo que el rol no puede hacer, ni siquiera compila.");
    }
}
