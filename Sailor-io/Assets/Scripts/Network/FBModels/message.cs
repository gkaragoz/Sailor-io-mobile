// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace SailorIO.Models
{

using global::System;
using global::FlatBuffers;

public enum EventTypes : sbyte
{
 UpdateModel = 0,
 WorldInfoUpdate = 1,
 NewPlayer = 2,
 BuyNewShip = 3,
 SailShip = 4,
 RemovePlayer = 5,
 PlayerDisconnect = 6,
};

public enum SupplyTypes : sbyte
{
 CRATE1 = 0,
 CRATE2 = 1,
 WOODENBARREL1 = 2,
 WOODENBARREL2 = 3,
};

public enum ShipTypes : sbyte
{
 RAFT1 = 0,
};

public struct Vec3 : IFlatbufferObject
{
  private Struct __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public Vec3 __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public float X { get { return __p.bb.GetFloat(__p.bb_pos + 0); } }
  public float Y { get { return __p.bb.GetFloat(__p.bb_pos + 4); } }
  public float Z { get { return __p.bb.GetFloat(__p.bb_pos + 8); } }

  public static Offset<Vec3> CreateVec3(FlatBufferBuilder builder, float X, float Y, float Z) {
    builder.Prep(4, 12);
    builder.PutFloat(Z);
    builder.PutFloat(Y);
    builder.PutFloat(X);
    return new Offset<Vec3>(builder.Offset);
  }
};

public struct Input : IFlatbufferObject
{
  private Struct __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public Input __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Vec3 Pos { get { return (new Vec3()).__assign(__p.bb_pos + 0, __p.bb); } }
  public int Time { get { return __p.bb.GetInt(__p.bb_pos + 12); } }
  public int DeltaTime { get { return __p.bb.GetInt(__p.bb_pos + 16); } }
  public int SequenceId { get { return __p.bb.GetInt(__p.bb_pos + 20); } }

  public static Offset<Input> CreateInput(FlatBufferBuilder builder, float pos_X, float pos_Y, float pos_Z, int Time, int DeltaTime, int SequenceId) {
    builder.Prep(4, 24);
    builder.PutInt(SequenceId);
    builder.PutInt(DeltaTime);
    builder.PutInt(Time);
    builder.Prep(4, 12);
    builder.PutFloat(pos_Z);
    builder.PutFloat(pos_Y);
    builder.PutFloat(pos_X);
    return new Offset<Input>(builder.Offset);
  }
};

public struct Supply : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static Supply GetRootAsSupply(ByteBuffer _bb) { return GetRootAsSupply(_bb, new Supply()); }
  public static Supply GetRootAsSupply(ByteBuffer _bb, Supply obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public Supply __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Vec3? Pos { get { int o = __p.__offset(4); return o != 0 ? (Vec3?)(new Vec3()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public SupplyTypes AssetId { get { int o = __p.__offset(6); return o != 0 ? (SupplyTypes)__p.bb.GetSbyte(o + __p.bb_pos) : SupplyTypes.CRATE1; } }
  public bool IsDeath { get { int o = __p.__offset(8); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public bool IsNew { get { int o = __p.__offset(10); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }

  public static void StartSupply(FlatBufferBuilder builder) { builder.StartObject(4); }
  public static void AddPos(FlatBufferBuilder builder, Offset<Vec3> posOffset) { builder.AddStruct(0, posOffset.Value, 0); }
  public static void AddAssetId(FlatBufferBuilder builder, SupplyTypes assetId) { builder.AddSbyte(1, (sbyte)assetId, 0); }
  public static void AddIsDeath(FlatBufferBuilder builder, bool isDeath) { builder.AddBool(2, isDeath, false); }
  public static void AddIsNew(FlatBufferBuilder builder, bool isNew) { builder.AddBool(3, isNew, false); }
  public static Offset<Supply> EndSupply(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<Supply>(o);
  }
};

public struct Ship : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static Ship GetRootAsShip(ByteBuffer _bb) { return GetRootAsShip(_bb, new Ship()); }
  public static Ship GetRootAsShip(ByteBuffer _bb, Ship obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public Ship __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public Vec3? Pos { get { int o = __p.__offset(4); return o != 0 ? (Vec3?)(new Vec3()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public float ViewAngle { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public int Id { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public ShipTypes AssetType { get { int o = __p.__offset(10); return o != 0 ? (ShipTypes)__p.bb.GetSbyte(o + __p.bb_pos) : ShipTypes.RAFT1; } }
  public int CaptainUserId { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int CurrentSuppliesCount { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int CurrentSailorsCount { get { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int CurrentHealth { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public float SlopeSpeed { get { int o = __p.__offset(20); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public float RotationSpeed { get { int o = __p.__offset(22); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public float MovementSpeed { get { int o = __p.__offset(24); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }

  public static void StartShip(FlatBufferBuilder builder) { builder.StartObject(11); }
  public static void AddPos(FlatBufferBuilder builder, Offset<Vec3> posOffset) { builder.AddStruct(0, posOffset.Value, 0); }
  public static void AddViewAngle(FlatBufferBuilder builder, float viewAngle) { builder.AddFloat(1, viewAngle, 0.0f); }
  public static void AddId(FlatBufferBuilder builder, int Id) { builder.AddInt(2, Id, 0); }
  public static void AddAssetType(FlatBufferBuilder builder, ShipTypes assetType) { builder.AddSbyte(3, (sbyte)assetType, 0); }
  public static void AddCaptainUserId(FlatBufferBuilder builder, int captainUserId) { builder.AddInt(4, captainUserId, 0); }
  public static void AddCurrentSuppliesCount(FlatBufferBuilder builder, int currentSuppliesCount) { builder.AddInt(5, currentSuppliesCount, 0); }
  public static void AddCurrentSailorsCount(FlatBufferBuilder builder, int currentSailorsCount) { builder.AddInt(6, currentSailorsCount, 0); }
  public static void AddCurrentHealth(FlatBufferBuilder builder, int currentHealth) { builder.AddInt(7, currentHealth, 0); }
  public static void AddSlopeSpeed(FlatBufferBuilder builder, float slopeSpeed) { builder.AddFloat(8, slopeSpeed, 0.0f); }
  public static void AddRotationSpeed(FlatBufferBuilder builder, float rotationSpeed) { builder.AddFloat(9, rotationSpeed, 0.0f); }
  public static void AddMovementSpeed(FlatBufferBuilder builder, float movementSpeed) { builder.AddFloat(10, movementSpeed, 0.0f); }
  public static Offset<Ship> EndShip(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<Ship>(o);
  }
};

public struct WorldInfoTable : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static WorldInfoTable GetRootAsWorldInfoTable(ByteBuffer _bb) { return GetRootAsWorldInfoTable(_bb, new WorldInfoTable()); }
  public static WorldInfoTable GetRootAsWorldInfoTable(ByteBuffer _bb, WorldInfoTable obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public WorldInfoTable __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UserSlotId { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Height { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Width { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int Length { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int OffSetX { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int OffSetZ { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int OffSetY { get { int o = __p.__offset(16); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<WorldInfoTable> CreateWorldInfoTable(FlatBufferBuilder builder,
      int userSlotId = 0,
      int height = 0,
      int width = 0,
      int length = 0,
      int offSetX = 0,
      int offSetZ = 0,
      int offSetY = 0) {
    builder.StartObject(7);
    WorldInfoTable.AddOffSetY(builder, offSetY);
    WorldInfoTable.AddOffSetZ(builder, offSetZ);
    WorldInfoTable.AddOffSetX(builder, offSetX);
    WorldInfoTable.AddLength(builder, length);
    WorldInfoTable.AddWidth(builder, width);
    WorldInfoTable.AddHeight(builder, height);
    WorldInfoTable.AddUserSlotId(builder, userSlotId);
    return WorldInfoTable.EndWorldInfoTable(builder);
  }

  public static void StartWorldInfoTable(FlatBufferBuilder builder) { builder.StartObject(7); }
  public static void AddUserSlotId(FlatBufferBuilder builder, int userSlotId) { builder.AddInt(0, userSlotId, 0); }
  public static void AddHeight(FlatBufferBuilder builder, int height) { builder.AddInt(1, height, 0); }
  public static void AddWidth(FlatBufferBuilder builder, int width) { builder.AddInt(2, width, 0); }
  public static void AddLength(FlatBufferBuilder builder, int length) { builder.AddInt(3, length, 0); }
  public static void AddOffSetX(FlatBufferBuilder builder, int offSetX) { builder.AddInt(4, offSetX, 0); }
  public static void AddOffSetZ(FlatBufferBuilder builder, int offSetZ) { builder.AddInt(5, offSetZ, 0); }
  public static void AddOffSetY(FlatBufferBuilder builder, int offSetY) { builder.AddInt(6, offSetY, 0); }
  public static Offset<WorldInfoTable> EndWorldInfoTable(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<WorldInfoTable>(o);
  }
};

public struct RemovePlayerInfoTable : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static RemovePlayerInfoTable GetRootAsRemovePlayerInfoTable(ByteBuffer _bb) { return GetRootAsRemovePlayerInfoTable(_bb, new RemovePlayerInfoTable()); }
  public static RemovePlayerInfoTable GetRootAsRemovePlayerInfoTable(ByteBuffer _bb, RemovePlayerInfoTable obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public RemovePlayerInfoTable __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UserSlotId { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<RemovePlayerInfoTable> CreateRemovePlayerInfoTable(FlatBufferBuilder builder,
      int userSlotId = 0) {
    builder.StartObject(1);
    RemovePlayerInfoTable.AddUserSlotId(builder, userSlotId);
    return RemovePlayerInfoTable.EndRemovePlayerInfoTable(builder);
  }

  public static void StartRemovePlayerInfoTable(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddUserSlotId(FlatBufferBuilder builder, int userSlotId) { builder.AddInt(0, userSlotId, 0); }
  public static Offset<RemovePlayerInfoTable> EndRemovePlayerInfoTable(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<RemovePlayerInfoTable>(o);
  }
};

public struct UpdateModel : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static UpdateModel GetRootAsUpdateModel(ByteBuffer _bb) { return GetRootAsUpdateModel(_bb, new UpdateModel()); }
  public static UpdateModel GetRootAsUpdateModel(ByteBuffer _bb, UpdateModel obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public UpdateModel __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public EventTypes EventType { get { int o = __p.__offset(4); return o != 0 ? (EventTypes)__p.bb.GetSbyte(o + __p.bb_pos) : EventTypes.UpdateModel; } }
  public short UpdatePassTime { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetShort(o + __p.bb_pos) : (short)0; } }
  public double UpdateTime { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetDouble(o + __p.bb_pos) : (double)0.0; } }
  public Supply? SupplyCrates(int j) { int o = __p.__offset(10); return o != 0 ? (Supply?)(new Supply()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int SupplyCratesLength { get { int o = __p.__offset(10); return o != 0 ? __p.__vector_len(o) : 0; } }
  public Ship? ShipModels(int j) { int o = __p.__offset(12); return o != 0 ? (Ship?)(new Ship()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int ShipModelsLength { get { int o = __p.__offset(12); return o != 0 ? __p.__vector_len(o) : 0; } }
  public WorldInfoTable? WorldInfo { get { int o = __p.__offset(14); return o != 0 ? (WorldInfoTable?)(new WorldInfoTable()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public RemovePlayerInfoTable? RemovePlayerInfo { get { int o = __p.__offset(16); return o != 0 ? (RemovePlayerInfoTable?)(new RemovePlayerInfoTable()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<UpdateModel> CreateUpdateModel(FlatBufferBuilder builder,
      EventTypes eventType = EventTypes.UpdateModel,
      short updatePassTime = 0,
      double updateTime = 0.0,
      VectorOffset supplyCratesOffset = default(VectorOffset),
      VectorOffset shipModelsOffset = default(VectorOffset),
      Offset<WorldInfoTable> worldInfoOffset = default(Offset<WorldInfoTable>),
      Offset<RemovePlayerInfoTable> removePlayerInfoOffset = default(Offset<RemovePlayerInfoTable>)) {
    builder.StartObject(7);
    UpdateModel.AddUpdateTime(builder, updateTime);
    UpdateModel.AddRemovePlayerInfo(builder, removePlayerInfoOffset);
    UpdateModel.AddWorldInfo(builder, worldInfoOffset);
    UpdateModel.AddShipModels(builder, shipModelsOffset);
    UpdateModel.AddSupplyCrates(builder, supplyCratesOffset);
    UpdateModel.AddUpdatePassTime(builder, updatePassTime);
    UpdateModel.AddEventType(builder, eventType);
    return UpdateModel.EndUpdateModel(builder);
  }

  public static void StartUpdateModel(FlatBufferBuilder builder) { builder.StartObject(7); }
  public static void AddEventType(FlatBufferBuilder builder, EventTypes eventType) { builder.AddSbyte(0, (sbyte)eventType, 0); }
  public static void AddUpdatePassTime(FlatBufferBuilder builder, short updatePassTime) { builder.AddShort(1, updatePassTime, 0); }
  public static void AddUpdateTime(FlatBufferBuilder builder, double updateTime) { builder.AddDouble(2, updateTime, 0.0); }
  public static void AddSupplyCrates(FlatBufferBuilder builder, VectorOffset supplyCratesOffset) { builder.AddOffset(3, supplyCratesOffset.Value, 0); }
  public static VectorOffset CreateSupplyCratesVector(FlatBufferBuilder builder, Offset<Supply>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartSupplyCratesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddShipModels(FlatBufferBuilder builder, VectorOffset shipModelsOffset) { builder.AddOffset(4, shipModelsOffset.Value, 0); }
  public static VectorOffset CreateShipModelsVector(FlatBufferBuilder builder, Offset<Ship>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartShipModelsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddWorldInfo(FlatBufferBuilder builder, Offset<WorldInfoTable> worldInfoOffset) { builder.AddOffset(5, worldInfoOffset.Value, 0); }
  public static void AddRemovePlayerInfo(FlatBufferBuilder builder, Offset<RemovePlayerInfoTable> removePlayerInfoOffset) { builder.AddOffset(6, removePlayerInfoOffset.Value, 0); }
  public static Offset<UpdateModel> EndUpdateModel(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<UpdateModel>(o);
  }
  public static void FinishUpdateModelBuffer(FlatBufferBuilder builder, Offset<UpdateModel> offset) { builder.Finish(offset.Value); }
};


}
