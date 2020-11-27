using System.Collections.Generic;

public interface IListenToEvents{
    void OnGameEvent( GameEvent gameEvent );
}

public struct GameEvent{
    public GameEventType type;
    public object parameter;

    public GameEvent( GameEventType _type, object _parameter ){
        parameter = _parameter;
        type = _type;
    }

    public GameEvent( GameEventType _type){
        parameter = null;
        type = _type;
    }
};

public static class GameEventSystem
{
    static Dictionary<GameEventType, List<IListenToEvents>> _registeredObjects = new Dictionary<GameEventType, List<IListenToEvents>>(); 

    public static void RegisterListener( IListenToEvents newListener, GameEventType type){
        if( !_registeredObjects.ContainsKey(type) ){
            _registeredObjects[type] = new List<IListenToEvents>(){ newListener };
        }else{
            _registeredObjects[type].Add(newListener);
        }
    }

    public static void DeregisterListener( IListenToEvents newListener, GameEventType type ){
        if( _registeredObjects.TryGetValue( type, out List<IListenToEvents> list ) ){
            list.Remove( newListener );
        }
    }

    public static void RiseEvent( GameEvent gameEvent ){
        if( _registeredObjects.TryGetValue( gameEvent.type, out List<IListenToEvents> list ) ){
            for( int i = 0; i < list.Count; i++){
                list[i].OnGameEvent(gameEvent);
            }
        }
    }
}
