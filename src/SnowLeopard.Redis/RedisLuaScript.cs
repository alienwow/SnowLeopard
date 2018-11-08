namespace SnowLeopard.Redis
{
    internal static class RedisLuaScript
    {
        #region LuaScript

        internal const string KEY_DELETE_SCRIPT = @"local ks = redis.call('KEYS', @keypattern)
for i=1,#ks,5000 do 
  redis.call('del', unpack(ks, i, math.min(i+4999, #ks)))
end 
return true";

        #endregion

    }
}
