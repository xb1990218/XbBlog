using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class BaseService<T> where T : class, new()
    {
        public EFContext db;
        /// <summary>
        /// 增(异步)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<T> AddAsync(T entity)
        {
            db.Set<T>().Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T Add(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
            return entity;
        }

        /// <summary>
        ///删除一条实体数据（异步）
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<bool> DelAsync(T entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
            int i = await db.SaveChangesAsync();
            return i > 0;
        }

        /// <summary>
        /// 删除一条实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Del(T entity)
        {
            db.Entry(entity).State = EntityState.Deleted;
            int i = db.SaveChanges();
            return i > 0;
        }

        /// <summary>
        /// 删除多条数据，根据传进来的id数据删除(异步)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual async Task<bool> DelByIdsAsync(params int[] ids)
        {
            foreach (int id in ids)
            {
                var entity = db.Set<T>().Find(id);//如果实体已经在内存中则直接从内存中拿，否则才查询数据库
                db.Set<T>().Remove(entity);
            }
            int i = await db.SaveChangesAsync();
            return i > 0;
        }

        /// <summary>
        /// 删除多条数据，根据传进来的id数据删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual bool DelByIds(params int[] ids)
        {
            foreach (int id in ids)
            {
                var entity = db.Set<T>().Find(id);//如果实体已经在内存中则直接从内存中拿，否则才查询数据库
                db.Set<T>().Remove(entity);
            }
            int i = db.SaveChanges();
            return i > 0;
        }

        /// <summary>
        /// 改(异步)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<bool> EditAsync(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            int i = await db.SaveChangesAsync();
            return i > 0;
        }

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Edit(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            int i = db.SaveChanges();
            return i > 0;
        }

        /// <summary>
        /// 查-获取单条数据(异步)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public virtual async Task<T> GetEntityAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return await db.Set<T>().Where(whereLambda).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查-获取单条数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public virtual T GetEntity(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).FirstOrDefault();
        }

        /// <summary>
        /// 查-获取多条数据，返回list集合(异步)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetListEntitiesAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return await db.Set<T>().Where(whereLambda).ToListAsync();
        }

        /// <summary>
        /// 查-获取多条数据，返回list集合
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public virtual List<T> GetListEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).ToList();
        }

        /// <summary>
        /// 查询-分页(异步)
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页多少条</param>
        /// <param name="whereLambda">过滤数据的Lambda表达式</param>
        /// <param name="orderbyLambda">按照哪个排序</param>
        /// <param name="isAsc">是否升序排序：true升序、false降序</param>
        /// <returns></returns>
        public async Task<List<T>> GetPageListAsync<s>(int pageIndex, int pageSize,
            System.Linq.Expressions.Expression<Func<T, bool>> whereLambda,
            System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            List<T> list = null;
            if (isAsc)//升序
            {
                list = await db.Set<T>().Where(whereLambda).OrderBy(orderbyLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else//降序
            {
                list = await db.Set<T>().Where(whereLambda).OrderByDescending(orderbyLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            return list;
        }

        /// <summary>
        /// 查询-分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public List<T> GetPageList<s>(int pageIndex, int pageSize,
            System.Linq.Expressions.Expression<Func<T, bool>> whereLambda,
            System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc, out int totalCount)
        {
            List<T> list = null;
            totalCount = db.Set<T>().Where(whereLambda).Count();
            if (isAsc)//升序
            {
                list = db.Set<T>().Where(whereLambda).OrderBy(orderbyLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else//降序
            {
                list = db.Set<T>().Where(whereLambda).OrderByDescending(orderbyLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取总条数(异步)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<int> GetTotalCountAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await db.Set<T>().Where(whereLambda).CountAsync();
        }
    }
}
