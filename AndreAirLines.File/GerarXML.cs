using AndreAirLines.Model.Entities;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace AndreAirLines.File
{
    public static class GerarXML
    {

        public static XElement VoosToXML(List<Voo> voos)
        {
            if (voos != null)
            {

                var voosToXML = new XElement("Root",
                    from data in voos
                    select new XElement("voos",
                               new XElement("id", data.Id),
                               new XElement("destino", AeroportoToXML(data.Destino)),
                               new XElement("origem", AeroportoToXML(data.Origem)),
                               new XElement("horarioEmbarque", data.HorarioEmbarque),
                               new XElement("horarioDesembarque", data.HorarioEmbarque)
                            )
                        );

                return voosToXML;
            }

            return null;
        }



        public static XElement AeronaveToXML(Aeronave aeronave)
        {
            if (aeronave != null)
            {
                var aeronaveToXML = new XElement("aeronave",
                               new XElement("id", aeronave.Id),
                               new XElement("nome", aeronave.Nome),
                               new XElement("capacidade", aeronave.Capacidade)
                            );

                return aeronaveToXML;
            }

            return null;
        }

        public static XElement AeroportoToXML(Aeroporto aeroporto)
        {
            if (aeroporto != null)
            {
                var aeroportoToXML = new XElement("aeroporto",
                               new XElement("sigla", aeroporto.Sigla),
                               new XElement("nome", aeroporto.Nome)
                            );

                return aeroportoToXML;
            }

            return null;
        }

        public static XElement EnderecoToXML(Endereco endereco)
        {
            if (endereco != null)
            {
                var enderecoToXML = new XElement("endereco",
                               new XElement("id", endereco.Id),
                               new XElement("bairro", endereco.Bairro),
                               new XElement("pais", endereco.Pais),
                               new XElement("cep", endereco.Cidade),
                               new XElement("logradouro", endereco.Logradouro),
                               new XElement("estado", endereco.Estado),
                               new XElement("numero", endereco.Numero),
                               new XElement("complemento", endereco.Complemento)

                            );

                return enderecoToXML;
            }

            return null;
        }

        /*public static void Create<Entity>(IEnumerable<Entity> entities) where Entity : class
        {
            // Allocate the XDocument and add an XML declaration.  
            XDocument RejectedXmlList = new XDocument(new XDeclaration("1.0", "utf-8", null));

            // At this point RejectedXmlList.Root is still null, so add a unique root element.
            RejectedXmlList.Add(new XElement("Rejectedparameters"));

            // Add elements for each Parameter to the root element
            foreach (Entity Myparameter in entities)
            {
                if (true)
                {
                    XElement xelement = new XElement(Myparameter, CurrentData.ToString());
                    RejectedXmlList.Root.Add(xelement);
                }
            }
        }*/
    }
}
