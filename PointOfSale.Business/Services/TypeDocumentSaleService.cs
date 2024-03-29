﻿using Microsoft.EntityFrameworkCore;
using PointOfSale.Business.Contracts;
using PointOfSale.Data.Repository;
using PointOfSale.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Business.Services
{
    public class TypeDocumentSaleService : ITypeDocumentSaleService
    {
        private readonly IGenericRepository<TypeDocumentSale> _repository;

        public TypeDocumentSaleService(IGenericRepository<TypeDocumentSale> repository)
        {
            _repository = repository;
        }

		public async Task<List<TypeDocumentSale>> List()
        {
            IQueryable<TypeDocumentSale> query = await _repository.Query();
            return query.ToList();
        }

        public async Task<List<TypeDocumentSale>> ListForTypeDocumentSales()
        {
            IQueryable<TypeDocumentSale> query = await _repository.Query();
            List<TypeDocumentSale> TypeDocumentSales = await query
                .Where(dv => dv.IsActive == true)
                .ToListAsync();

            return TypeDocumentSales;
        }

        public async Task<TypeDocumentSale> Add(TypeDocumentSale entity)
        {
            TypeDocumentSale TypeDocumentSale_exists = await _repository.Get(u => u.IdTypeDocumentSale == entity.IdTypeDocumentSale);

            if (TypeDocumentSale_exists != null)
                throw new TaskCanceledException("The Type Document Sale already exists");

            try
            {
                TypeDocumentSale TypeDocumentSale_created = await _repository.Add(entity);

                if (TypeDocumentSale_created.IdTypeDocumentSale == 0)
                    throw new TaskCanceledException("Failed to create Type Document Sale");

                IQueryable<TypeDocumentSale> query = await _repository.Query(u => u.IdTypeDocumentSale == TypeDocumentSale_created.IdTypeDocumentSale);
                TypeDocumentSale_created = query.First();

                return TypeDocumentSale_created;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TypeDocumentSale> Edit(TypeDocumentSale entity)
        {
            TypeDocumentSale TypeDocumentSale_exists = await _repository.Get(u => u.IdTypeDocumentSale == entity.IdTypeDocumentSale && u.Description != entity.Description);

            if (TypeDocumentSale_exists != null)
                throw new TaskCanceledException("The Type Document Sale already exists");

            try
            {
                IQueryable<TypeDocumentSale> queryTypeDocumentSale = await _repository.Query(u => u.IdTypeDocumentSale == entity.IdTypeDocumentSale);

                TypeDocumentSale TypeDocumentSale_edit = queryTypeDocumentSale.First();

                TypeDocumentSale_edit.Description = entity.Description;
                TypeDocumentSale_edit.IsActive = entity.IsActive;

                bool response = await _repository.Edit(TypeDocumentSale_edit);
                if (!response)
                    throw new TaskCanceledException("Could not modify Type Document Sale");

                TypeDocumentSale TypeDocumentSale_edited = queryTypeDocumentSale.First();

                return TypeDocumentSale_edit;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int idTypeDocumentSale)
        {
            try
            {
                TypeDocumentSale TypeDocumentSale_found = await _repository.Get(u => u.IdTypeDocumentSale == idTypeDocumentSale);

                if (TypeDocumentSale_found == null)
                    throw new TaskCanceledException("Type Document Sale does not exist");

                bool response = await _repository.Delete(TypeDocumentSale_found);

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
	}
}
