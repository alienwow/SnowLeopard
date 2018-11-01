﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace SnowLeopard.Redis
{
    public interface IRedisCache
    {
        #region Key（键）完事儿

        /// <summary>
        /// KeyDelete
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        bool KeyDelete(string key, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// KeyDelete
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        long KeyDelete(string[] keys, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// KeyDump
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        byte[] KeyDump(string key, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// KeyExists
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        bool KeyExists(string key, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// KeyExists
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        bool KeyExpire(string key, TimeSpan? expiry, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// KeyExpire
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        bool KeyExpire(string key, DateTime? expiry, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// 模糊匹配查询Key
        /// </summary>
        /// <param name="hostandport"></param>
        /// <param name="pattern"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        IEnumerable<RedisKey> Keys(string hostandport, string pattern, int db = 0);

        //EXPIREAT          在哪里？
        //KeyMigrate
        //KeyMigrateAsync
        //KeyMove
        //KeyMoveAsync
        //OBJECT            在哪里？
        //KeyPersist
        //KeyPersistAsync
        //PEXPIRE           在哪里？
        //PEXPIREAT         在哪里？
        //PTTL              在哪里？
        //KeyRandom
        //KeyRandomAsync
        //KeyRename
        //KeyRenameAsync
        //RENAMENX          在哪里？
        //KeyRestore
        //KeyRestoreAsync
        //Sort
        //SortAsync
        //TTL               在哪里？
        //KeyType
        //KeyTypeAsync
        //SCAN              在哪里？
        #endregion

        #region Object

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        T Get<T>(string key, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        T[] Get<T>(string[] keys, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// Set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="timeSpan"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        void Set<T>(string key, T value, int db = 0, TimeSpan? timeSpan = null, When when = When.Always, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// Increment
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        long Increment(string key, long value = 1, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// Decrement
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        long Decrement(string key, long value = 1, int db = 0, CommandFlags flags = CommandFlags.None);

        #endregion

        #region String
        ///APPEND
        ///BITCOUNT
        ///BITOP
        ///BITFIELD
        ///DECR
        ///DECRBY
        ///GET
        ///GETBIT
        ///GETRANGE
        ///GETSET
        ///INCR
        ///INCRBY
        ///INCRBYFLOAT
        ///MGET
        ///MSET
        ///MSETNX
        ///PSETEX
        ///SET
        ///SETBIT
        ///SETEX
        ///SETNX
        ///SETRANGE
        ///STRLEN
        #endregion

        #region Hash完事儿
        /// <summary>
        /// HDel
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashid"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        bool HDel(string key, string hashid, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HDel
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashids"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        long HDel(string key, string[] hashids, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HExists
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashid"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        bool HExists(string key, string hashid, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HGet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashid"></param>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        T HGet<T>(string key, string hashid, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HGet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashids"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        IDictionary<string, T> HGet<T>(string key, string[] hashids, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HGetAll
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        IDictionary<string, T> HGetAll<T>(string key, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HIncrBy
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashid"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        long HIncrBy(string key, string hashid, long value = 1, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HIncrByDouble
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashid"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        double HIncrByDouble(string key, string hashid, double value = 1.0, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HDecrBy
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashid"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        long HDecrBy(string key, string hashid, long value = 1, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HDecrByDouble
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashid"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        double HDecrByDouble(string key, string hashid, double value = 1.0, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HKeys
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        string[] HKeys(string key, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HLen
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        long HLen(string key, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashid"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        bool HSet<T>(string key, string hashid, T value, int db = 0, When when = When.Always, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dic"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        void HSet<T>(string key, IDictionary<string, T> dic, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HValues
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        T[] HValues<T>(string key, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// HScan
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        T[] HScan<T>(string key, string pattern, int db = 0);

        #endregion

        #region Set完事儿
        /// <summary>
        /// SAdd
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        void SAdd<T>(string key, T value, int db = 0, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// SAddAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        Task<bool> SAddAsync<T>(string key, T value, int db = 0, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// Set元素数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        void SCard(string key, int db = 0, CommandFlags flags = CommandFlags.None);

        ///SDIFF
        ///SDIFFSTORE
        ///SINTER
        ///SINTERSTORE
        ///SISMEMBER
        ///SMEMBERS
        ///SMOVE
        ///SPOP
        ///SRANDMEMBER
        ///SREM
        ///SUNION
        ///SUNIONSTORE
        ///SSCAN

        /// <summary>
        /// SPop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        T SPop<T>(string key, int db = 0, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// SRemove
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        bool SRemove<T>(string key, T value, int db = 0, CommandFlags flags = CommandFlags.None);
        /// <summary>
        /// SRemoveAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        Task<bool> SRemoveAsync<T>(string key, T value, int db = 0, CommandFlags flags = CommandFlags.None);

        #endregion

        #region List

        /// <summary>
        /// LPush
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        void LPush<T>(string key, T value, int db = 0, When when = When.Always, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// RPop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        T RPop<T>(string key, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// RPush
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        void RPush<T>(string key, T value, int db = 0, When when = When.Always, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// LPop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        T LPop<T>(string key, int db = 0, CommandFlags flags = CommandFlags.None);

        #endregion

        #region Subscriber

        /// <summary>
        /// GetSubscriber
        /// </summary>
        /// <returns></returns>
        ISubscriber GetSubscriber();

        /// <summary>
        /// Publish
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        long Publish<T>(string key, T value, int db = 0, CommandFlags flags = CommandFlags.None);

        /// <summary>
        /// PublishString
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        long PublishString(string key, string value, int db = 0, CommandFlags flags = CommandFlags.None);

        #endregion
    }
}
