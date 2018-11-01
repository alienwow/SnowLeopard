using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SnowLeopard.Lynx;
using StackExchange.Redis;

// VSCode 正则替换生成接口
// from: public (.*\))\n\s+\{[a-zA-Z0-9=_\(\);+,\.\n\s\[\]<>\{\}\/\u4e00-\u9fa5]+\}
// to: $1;

namespace SnowLeopard.Redis
{
    /// <summary>
    /// RedisCache
    /// </summary>
    public class RedisCache : IRedisCache
    {
        #region StackExchangeRedis

        public const string DEFAULT_CONNECTIONSTRING = "localhost:6379,name=SnowLeopard.Redis,connectTimeout=5000,password=";

        private readonly ConnectionMultiplexer _connection;
        private readonly RedisOption _redisOption;
        private readonly string _instanceName;

        public RedisCache(
            ConnectionMultiplexer connectionMultiplexer,
            IOptions<RedisConfig> redisConfig,
            LynxUtils lynxUtils
            )
        {
            if (redisConfig != null && redisConfig.Value != null)
                _redisOption = redisConfig.Value.RedisOption;

            _instanceName = _redisOption.InstanceName;
            if (string.IsNullOrEmpty(_instanceName))
                _instanceName = lynxUtils.EntryAssemblyName.Name;

            _instanceName += ":";

            _connection = connectionMultiplexer;
        }

        /// <summary>
        /// Json序列化时间格式
        /// </summary>
        public static readonly IsoDateTimeConverter TimeFormat = new IsoDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
        };

        #endregion

        #region Key（键）完事儿

        /// <summary>
        /// KeyDelete
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool KeyDelete(string key, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.KeyDelete(_instanceName + key, flags);
        }

        /// <summary>
        /// KeyDelete
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long KeyDelete(string[] keys, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            var keyArray = new RedisKey[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                keyArray[i] = _instanceName + keys[i];
            }
            return database.KeyDelete(keyArray, flags);
        }

        /// <summary>
        /// KeyDump
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public byte[] KeyDump(string key, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.KeyDump(_instanceName + key, flags);
        }

        /// <summary>
        /// KeyExists
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool KeyExists(string key, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.KeyExists(_instanceName + key);
        }

        /// <summary>
        /// KeyExists
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan? expiry, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.KeyExpire(_instanceName + key, expiry, flags);
        }

        /// <summary>
        /// KeyExpire
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, DateTime? expiry, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.KeyExpire(_instanceName + key, expiry, flags);
        }

        /// <summary>
        /// 模糊匹配查询Key
        /// </summary>
        /// <param name="hostandport"></param>
        /// <param name="pattern"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public IEnumerable<RedisKey> Keys(string hostandport, string pattern, int db = 0)
        {
            return _connection.GetServer(hostandport).Keys(db, pattern);
        }

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
        public T Get<T>(string key, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return DeserializeObject<T>(database.StringGet(_instanceName + key, flags));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T[] Get<T>(string[] keys, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            var redisKeys = new RedisKey[keys.Length];
            for (int i = 0; i < keys.Length; i++)
                redisKeys[i] = _instanceName + keys[i];
            var redisVals = database.StringGet(redisKeys, flags);
            var res = new T[redisVals.Length];
            for (int i = 0; i < redisVals.Length; i++)
                res[i] = DeserializeObject<T>(redisVals[i]);
            return res;
        }

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
        public void Set<T>(string key, T value, int db = 0, TimeSpan? timeSpan = null, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            database.StringSet(_instanceName + key, SerializeObject(value), timeSpan, when, flags);
        }

        /// <summary>
        /// Increment
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long Increment(string key, long value = 1, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.StringIncrement(_instanceName + key, value, flags);
        }

        /// <summary>
        /// Decrement
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long Decrement(string key, long value = 1, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.StringDecrement(_instanceName + key, value, flags);
        }

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
        public bool HDel(string key, string hashid, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.HashDelete(_instanceName + key, hashid, flags);
        }

        /// <summary>
        /// HDel
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashids"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long HDel(string key, string[] hashids, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            var keyArray = new RedisValue[hashids.Length];
            for (int i = 0; i < hashids.Length; i++)
            {
                keyArray[i] = hashids[i];
            }
            return database.HashDelete(_instanceName + key, keyArray, flags);
        }

        /// <summary>
        /// HExists
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashid"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool HExists(string key, string hashid, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.HashExists(_instanceName + key, hashid, flags);
        }

        /// <summary>
        /// HGet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashid"></param>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T HGet<T>(string key, string hashid, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return DeserializeObject<T>(database.HashGet(_instanceName + key, hashid, flags));
        }

        /// <summary>
        /// HGet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashids"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public IDictionary<string, T> HGet<T>(string key, string[] hashids, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            var hashFields = new RedisValue[hashids.Length];
            for (int i = 0; i < hashids.Length; i++)
            {
                hashFields[i] = hashids[i];
            }
            var redisValues = database.HashGet(_instanceName + key, hashFields, flags);
            var resDic = new Dictionary<string, T>();
            for (int i = 0; i < redisValues.Length; i++)
            {
                if (redisValues[i].HasValue)
                    resDic.Add(hashids[i], DeserializeObject<T>(redisValues[i]));
            }
            return resDic;
        }

        /// <summary>
        /// HGetAll
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public IDictionary<string, T> HGetAll<T>(string key, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            HashEntry[] hashEntry = database.HashGetAll(_instanceName + key, flags);
            var resDic = new Dictionary<string, T>();
            foreach (var item in hashEntry)
            {
                resDic.Add(item.Name, DeserializeObject<T>(item.Value));
            }
            return resDic;
        }

        /// <summary>
        /// HIncrBy
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashid"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long HIncrBy(string key, string hashid, long value = 1, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.HashIncrement(_instanceName + key, hashid, value, flags);
        }

        /// <summary>
        /// HIncrByDouble
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashid"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public double HIncrByDouble(string key, string hashid, double value = 1.0, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.HashIncrement(_instanceName + key, hashid, value, flags);
        }

        /// <summary>
        /// HDecrBy
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashid"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long HDecrBy(string key, string hashid, long value = 1, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.HashDecrement(_instanceName + key, hashid, value, flags);
        }

        /// <summary>
        /// HDecrByDouble
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashid"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public double HDecrByDouble(string key, string hashid, double value = 1.0, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.HashDecrement(_instanceName + key, hashid, value, flags);
        }

        /// <summary>
        /// HKeys
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public string[] HKeys(string key, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            var redisValues = database.HashKeys(_instanceName + key, flags);
            var res = new string[redisValues.Length];
            for (int i = 0; i < redisValues.Length; i++)
            {
                res[i] = redisValues[i];
            }
            return res;
        }

        /// <summary>
        /// HLen
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long HLen(string key, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.HashLength(_instanceName + key, flags);
        }

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
        public bool HSet<T>(string key, string hashid, T value, int db = 0, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.HashSet(_instanceName + key, hashid, SerializeObject(value), when, flags);
        }

        /// <summary>
        /// HSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dic"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        public void HSet<T>(string key, IDictionary<string, T> dic, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            var hashEntry = new HashEntry[dic.Count];
            int i = 0;
            foreach (var item in dic)
            {
                hashEntry[i++] = new HashEntry(item.Key, SerializeObject(item.Value));
            }
            database.HashSet(_instanceName + key, hashEntry, flags);
        }

        /// <summary>
        /// HValues
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T[] HValues<T>(string key, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            var redisValues = database.HashValues(_instanceName + key, flags);

            var res = new T[redisValues.Length];
            for (int i = 0; i < redisValues.Length; i++)
            {
                res[i] = DeserializeObject<T>(redisValues[i]);
            }
            return res;
        }

        /// <summary>
        /// HScan
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="pattern"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public T[] HScan<T>(string key, string pattern, int db = 0)
        {
            //TODO 需要测试
            var database = _connection.GetDatabase(db);
            var values = database.HashScan(_instanceName + key, pattern);
            var res = new T[values.Count()];
            int i = 0;
            foreach (var item in values)
            {
                res[i++] = DeserializeObject<T>(item.Value);
            }
            return res;
        }

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
        public void SAdd<T>(string key, T value, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            database.SetAdd(_instanceName + key, SerializeObject(value), flags);
        }
        /// <summary>
        /// SAddAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public Task<bool> SAddAsync<T>(string key, T value, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.SetAddAsync(_instanceName + key, SerializeObject(value), flags);
        }
        /// <summary>
        /// Set元素数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        public void SCard(string key, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            database.SetLength(_instanceName + key, flags);
        }

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
        public T SPop<T>(string key, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return DeserializeObject<T>(database.SetPop(_instanceName + key, flags));
        }
        /// <summary>
        /// SRemove
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool SRemove<T>(string key, T value, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.SetRemove(_instanceName + key, SerializeObject(value), flags);
        }
        /// <summary>
        /// SRemoveAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public Task<bool> SRemoveAsync<T>(string key, T value, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.SetRemoveAsync(_instanceName + key, SerializeObject(value), flags);
        }

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
        public void LPush<T>(string key, T value, int db = 0, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            database.ListLeftPush(_instanceName + key, SerializeObject(value), when, flags);
        }

        /// <summary>
        /// RPop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T RPop<T>(string key, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return DeserializeObject<T>(database.ListRightPop(_instanceName + key, flags));
        }

        /// <summary>
        /// RPush
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        public void RPush<T>(string key, T value, int db = 0, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            database.ListRightPush(_instanceName + key, SerializeObject(value), when, flags);
        }

        /// <summary>
        /// LPop
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public T LPop<T>(string key, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return DeserializeObject<T>(database.ListLeftPop(_instanceName + key, flags));
        }

        #endregion

        #region Subscriber

        /// <summary>
        /// GetSubscriber
        /// </summary>
        /// <returns></returns>
        public ISubscriber GetSubscriber()
        {
            return _connection.GetSubscriber();
        }

        /// <summary>
        /// Publish
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long Publish<T>(string key, T value, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.Publish(_instanceName + key, SerializeObject(value), flags);
        }

        /// <summary>
        /// PublishString
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long PublishString(string key, string value, int db = 0, CommandFlags flags = CommandFlags.None)
        {
            var database = _connection.GetDatabase(db);
            return database.Publish(_instanceName + key, value, flags);
        }

        #endregion

        #region Json

        private T DeserializeObject<T>(string obj)
        {
            if (string.IsNullOrEmpty(obj))
                return default(T);
            return JsonConvert.DeserializeObject<T>(obj);
        }

        private string SerializeObject(object obj)
        {
            if (obj == null)
                return null;
            return JsonConvert.SerializeObject(obj, TimeFormat);
        }

        #endregion
    }
}
