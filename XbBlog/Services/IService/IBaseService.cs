using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.IService
{
    /// <summary>
    /// 基接口，操作数据库常用方法：增 删 改 查 分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T> where T : class, new()
    {
        /// <summary>
        /// 增(异步)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Add(T entity);


        /// <summary>
        /// 删除一条数据(异步)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> DelAsync(T entity);

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Del(T entity);

        /// <summary>
        /// 删除多条数据(异步)-根据传进来的id数据删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DelByIdsAsync(params int[] ids);

        /// <summary>
        /// 删除多条数据-根据传进来的id数据删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DelByIds(params int[] ids);

        /// <summary>
        /// 改(异步)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> EditAsync(T entity);

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Edit(T entity);

        /// <summary>
        /// 查-获取单条数据(异步)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<T> GetEntityAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 查-获取单条数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        T GetEntity(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 查-获取多条数据，返回list集合(异步)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<List<T>> GetListEntitiesAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 查-获取多条数据，返回list集合
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        List<T> GetListEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 查询-分页(异步)
        /// </summary>
        /// <typeparam name="s">排序字段的类型</typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页显示多少条</param>
        /// <param name="whereLambda">过滤条件lambda</param>
        /// <param name="orderbyLambda">排序字段</param>
        /// <param name="isAsc">是否升序 true升序 false降序</param>
        /// <returns></returns>
        Task<List<T>> GetPageListAsync<s>(int pageIndex, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 查询-分页
        /// </summary>
        /// <typeparam name="s">排序字段的类型</typeparam>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页显示多少条</param>
        /// <param name="whereLambda">过滤条件lambda</param>
        /// <param name="orderbyLambda">排序字段</param>
        /// <param name="isAsc">是否升序 true升序 false降序</param>
        /// <param name="totalCount">返回的总记录数</param>
        /// <returns>返回实体集合</returns>
        List<T> GetPageList<s>(int pageIndex, int pageSize, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc, out int totalCount);

        /// <summary>
        /// 获取总记录数(异步)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<int> GetTotalCountAsync(Expression<Func<T, bool>> whereLambda);
    }
}
