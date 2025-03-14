using Dapper;
using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes;
using GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes.Repository;
using GHOST.TalentosCortes.Infrastructured.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GHOST.TalentosCortes.Infrastructured.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(TalentosCortesContext context) : base(context)
        {

        }

       
        public IEnumerable<Dashboard> Getdashboard()
        {

            var sql = @"SELECT * FROM Dashboards";

            return Db.Database.GetDbConnection().Query<Dashboard>(sql);
        }

        public override IEnumerable<Cliente> GetAll()
        {
            var sql = "SELECT * FROM CLIENTES E ";// +
            //          "WHERE E.EXCLUIDO = 0 " +
            //          "ORDER BY E.DATAFIM DESC ";

            return Db.Database.GetDbConnection().Query<Cliente>(sql);
        }

        public IEnumerable<Genero> ObterGenero()
        {
            var sql = @"SELECT * FROM GENEROS";
            return Db.Database.GetDbConnection().Query<Genero>(sql);
        }

        public void AdicionarEndereco(Endereco endereco)
        {
            Db.Enderecos.Add(endereco);
        }

        public void AtualizarEndereco(Endereco endereco)
        {
            Db.Enderecos.Update(endereco);
        }

        public Endereco ObterEnderecoPorId(Guid id)
        {
            var sql = @"SELECT * FROM Enderecos E " +
                      "WHERE E.Id = @uid";

            var endereco = Db.Database.GetDbConnection().Query<Endereco>(sql, new { uid = id });

            return endereco.SingleOrDefault();
        }

        public IEnumerable<Dashboard> AdicionarClienteDashboard()
        {
            var sql = @"DECLARE @yearDate varchar(5)
                    DECLARE @monthDate varchar(5)
                    DECLARE @janeiro nvarchar(max)
                    DECLARE @fevereiro nvarchar(max)
                    DECLARE @marco nvarchar(max)
                    DECLARE @abril nvarchar(max)
                    DECLARE @maio nvarchar(max)
                    DECLARE @junho nvarchar(max)
                    DECLARE @julho nvarchar(max)
                    DECLARE @agosto nvarchar(max)
                    DECLARE @setembro nvarchar(max)
                    DECLARE @outubro nvarchar(max)
                    DECLARE @novembro nvarchar(max)
                    DECLARE @dezembro nvarchar(max)

                    EXECUTE [dbo].[PROC_Dashboard_Cliente] 
                       @yearDate OUTPUT
                      ,@monthDate OUTPUT
                      ,@janeiro OUTPUT
                      ,@fevereiro OUTPUT
                      ,@marco OUTPUT
                      ,@abril OUTPUT
                      ,@maio OUTPUT
                      ,@junho OUTPUT
                      ,@julho OUTPUT
                      ,@agosto OUTPUT
                      ,@setembro OUTPUT
                      ,@outubro OUTPUT
                      ,@novembro OUTPUT
                      ,@dezembro OUTPUT";

            return Db.Database.GetDbConnection().Query<Dashboard>(sql);


        }

        public IEnumerable<Cliente> ObterClientesPorMasterUser(Guid masterUserId)
        {
            var sql = @"SELECT * FROM CLIENTES E " +
                        "WHERE E.EXCLUIDO = 0 " +
                        "AND E.MASTERUSERID = @oid " +
                        "ORDER BY E.DATA DESC";

            return Db.Database.GetDbConnection().Query<Cliente>(sql, new { oid = masterUserId });
        }

        public Cliente ObterMeusClientesPorId(Guid id, Guid masterUserId)
        {
            var sql = @"SELECT * FROM CLIENTES E " +
                      "LEFT JOIN Enderecos EN " +
                      "ON E.Id = EN.ClienteId " +
                      "WHERE E.EXCLUIDO = 0 " +
                      "AND E.MasterUserID = @oid " +
                      "AND E.ID = @eid";

            var cliente = Db.Database.GetDbConnection().Query<Cliente, Endereco, Cliente>(sql,
                (e, en) =>
                {
                    if (en != null)
                        e.AtribuirEndereco(en);

                    return e;
                },
                new { oid = masterUserId, eid = id });

            return cliente.FirstOrDefault();
        }
     

        public override Cliente GetById(Guid id)
        {
            var sql = @"SELECT * FROM CLIENTES E " +
                      "LEFT JOIN Enderecos EN " +
                      "ON E.Id = EN.ClienteId " +
                      "WHERE E.Id = @uid";
      
            var cliente = Db.Database.GetDbConnection().Query<Cliente, Endereco, Cliente>(sql,
                (e, en) =>
                {
                    if (en != null)
                        e.AtribuirEndereco(en);

                    return e;
                }, new { uid = id });

            return cliente.FirstOrDefault();
        }

        public override void Delete(Guid id)
        {
            var cliente = GetById(id);
            cliente.ExcluirClientes();
            Update(cliente);
        }

        
    }
}