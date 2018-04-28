/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     28/04/2018 23:59:49                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIET_HDAU') and o.name = 'FK_CHITIET__CHITIET_H_HOADONAN')
alter table CHITIET_HDAU
   drop constraint FK_CHITIET__CHITIET_H_HOADONAN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIET_HDAU') and o.name = 'FK_CHITIET__CHITIET_H_MATHANG')
alter table CHITIET_HDAU
   drop constraint FK_CHITIET__CHITIET_H_MATHANG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIET_HDDC') and o.name = 'FK_CHITIET__CHITIET_H_HOADONDI')
alter table CHITIET_HDDC
   drop constraint FK_CHITIET__CHITIET_H_HOADONDI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIET_HDDC') and o.name = 'FK_CHITIET__CHITIET_H_CHUYENDI')
alter table CHITIET_HDDC
   drop constraint FK_CHITIET__CHITIET_H_CHUYENDI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIET_HDGU') and o.name = 'FK_CHITIET__CHITIET_H_HOADONGI')
alter table CHITIET_HDGU
   drop constraint FK_CHITIET__CHITIET_H_HOADONGI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIET_HDGU') and o.name = 'FK_CHITIET__CHITIET_H_LUOTGIAT')
alter table CHITIET_HDGU
   drop constraint FK_CHITIET__CHITIET_H_LUOTGIAT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIET_HDLT') and o.name = 'FK_CHITIET__CHITIET_H_HOADONLU')
alter table CHITIET_HDLT
   drop constraint FK_CHITIET__CHITIET_H_HOADONLU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CHITIET_HDLT') and o.name = 'FK_CHITIET__CHITIET_H_PHONG')
alter table CHITIET_HDLT
   drop constraint FK_CHITIET__CHITIET_H_PHONG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADON') and o.name = 'FK_HOADON_CO_HD_KHACHHAN')
alter table HOADON
   drop constraint FK_HOADON_CO_HD_KHACHHAN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADON') and o.name = 'FK_HOADON_CO_HDAU_HOADONAN')
alter table HOADON
   drop constraint FK_HOADON_CO_HDAU_HOADONAN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADON') and o.name = 'FK_HOADON_CO_HDDC_HOADONDI')
alter table HOADON
   drop constraint FK_HOADON_CO_HDDC_HOADONDI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADON') and o.name = 'FK_HOADON_CO_HDGU_HOADONGI')
alter table HOADON
   drop constraint FK_HOADON_CO_HDGU_HOADONGI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADON') and o.name = 'FK_HOADON_CO_HDLT_HOADONLU')
alter table HOADON
   drop constraint FK_HOADON_CO_HDLT_HOADONLU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('HOADON') and o.name = 'FK_HOADON_LAP_HD_NHANVIEN')
alter table HOADON
   drop constraint FK_HOADON_LAP_HD_NHANVIEN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LUOTGIATUI') and o.name = 'FK_LUOTGIAT_CO_LOAIGU_LOAIGIAT')
alter table LUOTGIATUI
   drop constraint FK_LUOTGIAT_CO_LOAIGU_LOAIGIAT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('NHANVIEN') and o.name = 'FK_NHANVIEN_CO_TK_TAIKHOAN')
alter table NHANVIEN
   drop constraint FK_NHANVIEN_CO_TK_TAIKHOAN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PHONG') and o.name = 'FK_PHONG_CO_LP_LOAIPHON')
alter table PHONG
   drop constraint FK_PHONG_CO_LP_LOAIPHON
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIET_HDAU')
            and   name  = 'CHITIET_HDAU2_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIET_HDAU.CHITIET_HDAU2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIET_HDAU')
            and   name  = 'CHITIET_HDAU_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIET_HDAU.CHITIET_HDAU_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHITIET_HDAU')
            and   type = 'U')
   drop table CHITIET_HDAU
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIET_HDDC')
            and   name  = 'CHITIET_HDDC2_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIET_HDDC.CHITIET_HDDC2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIET_HDDC')
            and   name  = 'CHITIET_HDDC_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIET_HDDC.CHITIET_HDDC_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHITIET_HDDC')
            and   type = 'U')
   drop table CHITIET_HDDC
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIET_HDGU')
            and   name  = 'CHITIET_HDGU2_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIET_HDGU.CHITIET_HDGU2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIET_HDGU')
            and   name  = 'CHITIET_HDGU_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIET_HDGU.CHITIET_HDGU_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHITIET_HDGU')
            and   type = 'U')
   drop table CHITIET_HDGU
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIET_HDLT')
            and   name  = 'CHITIET_HDLT2_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIET_HDLT.CHITIET_HDLT2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CHITIET_HDLT')
            and   name  = 'CHITIET_HDLT_FK'
            and   indid > 0
            and   indid < 255)
   drop index CHITIET_HDLT.CHITIET_HDLT_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHITIET_HDLT')
            and   type = 'U')
   drop table CHITIET_HDLT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CHUYENDI')
            and   type = 'U')
   drop table CHUYENDI
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADON')
            and   name  = 'CO_HDLT_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADON.CO_HDLT_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADON')
            and   name  = 'CO_HDAU_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADON.CO_HDAU_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADON')
            and   name  = 'CO_HDGU2_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADON.CO_HDGU2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADON')
            and   name  = 'CO_HDDC2_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADON.CO_HDDC2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADON')
            and   name  = 'CO_HD_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADON.CO_HD_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('HOADON')
            and   name  = 'LAP_HD_FK'
            and   indid > 0
            and   indid < 255)
   drop index HOADON.LAP_HD_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOADON')
            and   type = 'U')
   drop table HOADON
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOADONANUONG')
            and   type = 'U')
   drop table HOADONANUONG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOADONDICHUYEN')
            and   type = 'U')
   drop table HOADONDICHUYEN
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOADONGIATUI')
            and   type = 'U')
   drop table HOADONGIATUI
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HOADONLUUTRU')
            and   type = 'U')
   drop table HOADONLUUTRU
go

if exists (select 1
            from  sysobjects
           where  id = object_id('KHACHHANG')
            and   type = 'U')
   drop table KHACHHANG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LOAIGIATUI')
            and   type = 'U')
   drop table LOAIGIATUI
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LOAIPHONG')
            and   type = 'U')
   drop table LOAIPHONG
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('LUOTGIATUI')
            and   name  = 'CO_LOAIGU_FK'
            and   indid > 0
            and   indid < 255)
   drop index LUOTGIATUI.CO_LOAIGU_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LUOTGIATUI')
            and   type = 'U')
   drop table LUOTGIATUI
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MATHANG')
            and   type = 'U')
   drop table MATHANG
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('NHANVIEN')
            and   name  = 'CO_TK2_FK'
            and   indid > 0
            and   indid < 255)
   drop index NHANVIEN.CO_TK2_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('NHANVIEN')
            and   type = 'U')
   drop table NHANVIEN
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PHONG')
            and   name  = 'CO_LP_FK'
            and   indid > 0
            and   indid < 255)
   drop index PHONG.CO_LP_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PHONG')
            and   type = 'U')
   drop table PHONG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TAIKHOAN')
            and   type = 'U')
   drop table TAIKHOAN
go

/*==============================================================*/
/* Table: CHITIET_HDAU                                          */
/*==============================================================*/
create table CHITIET_HDAU (
   MA_HDAU              int                  not null,
   MA_MH                int                  not null,
   SOLUONG_MH           int                  null,
   constraint PK_CHITIET_HDAU primary key (MA_HDAU, MA_MH)
)
go

/*==============================================================*/
/* Index: CHITIET_HDAU_FK                                       */
/*==============================================================*/
create index CHITIET_HDAU_FK on CHITIET_HDAU (
MA_HDAU ASC
)
go

/*==============================================================*/
/* Index: CHITIET_HDAU2_FK                                      */
/*==============================================================*/
create index CHITIET_HDAU2_FK on CHITIET_HDAU (
MA_MH ASC
)
go

/*==============================================================*/
/* Table: CHITIET_HDDC                                          */
/*==============================================================*/
create table CHITIET_HDDC (
   MA_HDDC              int                  not null,
   MA_CD                int                  not null,
   SOLUONG_CD           int                  null,
   constraint PK_CHITIET_HDDC primary key (MA_HDDC, MA_CD)
)
go

/*==============================================================*/
/* Index: CHITIET_HDDC_FK                                       */
/*==============================================================*/
create index CHITIET_HDDC_FK on CHITIET_HDDC (
MA_HDDC ASC
)
go

/*==============================================================*/
/* Index: CHITIET_HDDC2_FK                                      */
/*==============================================================*/
create index CHITIET_HDDC2_FK on CHITIET_HDDC (
MA_CD ASC
)
go

/*==============================================================*/
/* Table: CHITIET_HDGU                                          */
/*==============================================================*/
create table CHITIET_HDGU (
   MA_HDGU              int                  not null,
   MA_LUOTGU            int                  not null,
   constraint PK_CHITIET_HDGU primary key (MA_HDGU, MA_LUOTGU)
)
go

/*==============================================================*/
/* Index: CHITIET_HDGU_FK                                       */
/*==============================================================*/
create index CHITIET_HDGU_FK on CHITIET_HDGU (
MA_HDGU ASC
)
go

/*==============================================================*/
/* Index: CHITIET_HDGU2_FK                                      */
/*==============================================================*/
create index CHITIET_HDGU2_FK on CHITIET_HDGU (
MA_LUOTGU ASC
)
go

/*==============================================================*/
/* Table: CHITIET_HDLT                                          */
/*==============================================================*/
create table CHITIET_HDLT (
   MA_HDLT              int                  not null,
   MA_PHONG             int                  not null,
   THOIGIANNHAN_PHONG   datetime             null,
   THOIGIANTRA_PHONG    datetime             null,
   constraint PK_CHITIET_HDLT primary key (MA_HDLT, MA_PHONG)
)
go

/*==============================================================*/
/* Index: CHITIET_HDLT_FK                                       */
/*==============================================================*/
create index CHITIET_HDLT_FK on CHITIET_HDLT (
MA_HDLT ASC
)
go

/*==============================================================*/
/* Index: CHITIET_HDLT2_FK                                      */
/*==============================================================*/
create index CHITIET_HDLT2_FK on CHITIET_HDLT (
MA_PHONG ASC
)
go

/*==============================================================*/
/* Table: CHUYENDI                                              */
/*==============================================================*/
create table CHUYENDI (
   MA_CD                int                  not null,
   SOLUONGNGUOI_CD      int                  null,
   DONGIA_CD            money                null,
   constraint PK_CHUYENDI primary key nonclustered (MA_CD)
)
go

/*==============================================================*/
/* Table: HOADON                                                */
/*==============================================================*/
create table HOADON (
   MA_HD                int                  not null,
   MA_HDAU              int                  null,
   MA_HDGU              int                  null,
   MA_HDLT              int                  not null,
   MA_HDDC              int                  null,
   MA_NV                int                  not null,
   MA_KH                int                  not null,
   THOIGIANLAP_HD       datetime             null,
   TRIGIA_HD            money                null,
   constraint PK_HOADON primary key nonclustered (MA_HD)
)
go

/*==============================================================*/
/* Index: LAP_HD_FK                                             */
/*==============================================================*/
create index LAP_HD_FK on HOADON (
MA_NV ASC
)
go

/*==============================================================*/
/* Index: CO_HD_FK                                              */
/*==============================================================*/
create index CO_HD_FK on HOADON (
MA_KH ASC
)
go

/*==============================================================*/
/* Index: CO_HDDC2_FK                                           */
/*==============================================================*/
create index CO_HDDC2_FK on HOADON (
MA_HDDC ASC
)
go

/*==============================================================*/
/* Index: CO_HDGU2_FK                                           */
/*==============================================================*/
create index CO_HDGU2_FK on HOADON (
MA_HDGU ASC
)
go

/*==============================================================*/
/* Index: CO_HDAU_FK                                            */
/*==============================================================*/
create index CO_HDAU_FK on HOADON (
MA_HDAU ASC
)
go

/*==============================================================*/
/* Index: CO_HDLT_FK                                            */
/*==============================================================*/
create index CO_HDLT_FK on HOADON (
MA_HDLT ASC
)
go

/*==============================================================*/
/* Table: HOADONANUONG                                          */
/*==============================================================*/
create table HOADONANUONG (
   MA_HDAU              int                  not null,
   THOIGIANLAP_HDAU     datetime             null,
   TRIGIA_HDAU          money                null,
   constraint PK_HOADONANUONG primary key nonclustered (MA_HDAU)
)
go

/*==============================================================*/
/* Table: HOADONDICHUYEN                                        */
/*==============================================================*/
create table HOADONDICHUYEN (
   MA_HDDC              int                  not null,
   THOIGIANLAP_HDDC     datetime             null,
   TRIGIA_HDDC          money                null,
   constraint PK_HOADONDICHUYEN primary key nonclustered (MA_HDDC)
)
go

/*==============================================================*/
/* Table: HOADONGIATUI                                          */
/*==============================================================*/
create table HOADONGIATUI (
   MA_HDGU              int                  not null,
   THOIGIANLAP_HDGU     datetime             null,
   TRIGIA_HDGU          money                null,
   constraint PK_HOADONGIATUI primary key nonclustered (MA_HDGU)
)
go

/*==============================================================*/
/* Table: HOADONLUUTRU                                          */
/*==============================================================*/
create table HOADONLUUTRU (
   MA_HDLT              int                  not null,
   THOIGIANLAP_HDLT     datetime             null,
   TRIGIA_HDLT          money                null,
   constraint PK_HOADONLUUTRU primary key nonclustered (MA_HDLT)
)
go

/*==============================================================*/
/* Table: KHACHHANG                                             */
/*==============================================================*/
create table KHACHHANG (
   MA_KH                int                  not null,
   HOTEN_KH             varchar(255)         null,
   SODIENTHOAI_KH       varchar(15)          null,
   CMND_KH              varchar(12)          null,
   constraint PK_KHACHHANG primary key nonclustered (MA_KH)
)
go

/*==============================================================*/
/* Table: LOAIGIATUI                                            */
/*==============================================================*/
create table LOAIGIATUI (
   MA_LOAIGU            int                  not null,
   TEN_LOAIGU           varchar(255)         null,
   DONGIA_LOAIGU        money                null,
   constraint PK_LOAIGIATUI primary key nonclustered (MA_LOAIGU)
)
go

/*==============================================================*/
/* Table: LOAIPHONG                                             */
/*==============================================================*/
create table LOAIPHONG (
   MA_LP                int                  not null,
   TEN_LP               varchar(255)         null,
   DONGIA_LP            money                null,
   constraint PK_LOAIPHONG primary key nonclustered (MA_LP)
)
go

/*==============================================================*/
/* Table: LUOTGIATUI                                            */
/*==============================================================*/
create table LUOTGIATUI (
   MA_LUOTGU            int                  not null,
   MA_LOAIGU            int                  not null,
   SOKILOGRAM_LUOTGU    int                  null,
   NGAYBATDAU_LUOTGU    datetime             null,
   NGAYKETTHUC_LUOTGU   datetime             null,
   constraint PK_LUOTGIATUI primary key nonclustered (MA_LUOTGU)
)
go

/*==============================================================*/
/* Index: CO_LOAIGU_FK                                          */
/*==============================================================*/
create index CO_LOAIGU_FK on LUOTGIATUI (
MA_LOAIGU ASC
)
go

/*==============================================================*/
/* Table: MATHANG                                               */
/*==============================================================*/
create table MATHANG (
   MA_MH                int                  not null,
   TEN_MH               varchar(255)         null,
   DONGIA_MH            money                null,
   NGAYNHAP_MH          datetime             null,
   constraint PK_MATHANG primary key nonclustered (MA_MH)
)
go

/*==============================================================*/
/* Table: NHANVIEN                                              */
/*==============================================================*/
create table NHANVIEN (
   MA_NV                int                  not null,
   MA_TK                int                  not null,
   HOTEN_NV             varchar(255)         null,
   GIOITINH_NV          bit                  null,
   NGAYSINH_NV          datetime             null,
   SODIENTHOAI_NV       varchar(15)          null,
   CHUCVU_NV            varchar(30)          null,
   DIACHI_NV            varchar(255)         null,
   NGAYVAOLAM_NV        datetime             null,
   constraint PK_NHANVIEN primary key nonclustered (MA_NV)
)
go

/*==============================================================*/
/* Index: CO_TK2_FK                                             */
/*==============================================================*/
create index CO_TK2_FK on NHANVIEN (
MA_TK ASC
)
go

/*==============================================================*/
/* Table: PHONG                                                 */
/*==============================================================*/
create table PHONG (
   MA_PHONG             int                  not null,
   MA_LP                int                  not null,
   TINHTRANG_PHONG      varchar(30)          null,
   constraint PK_PHONG primary key nonclustered (MA_PHONG)
)
go

/*==============================================================*/
/* Index: CO_LP_FK                                              */
/*==============================================================*/
create index CO_LP_FK on PHONG (
MA_LP ASC
)
go

/*==============================================================*/
/* Table: TAIKHOAN                                              */
/*==============================================================*/
create table TAIKHOAN (
   MA_TK                int                  not null,
   TENDANGNHAP_TK       varchar(32)          null,
   MATKHAU_TK           varchar(32)          null,
   constraint PK_TAIKHOAN primary key nonclustered (MA_TK)
)
go

alter table CHITIET_HDAU
   add constraint FK_CHITIET__CHITIET_H_HOADONAN foreign key (MA_HDAU)
      references HOADONANUONG (MA_HDAU)
go

alter table CHITIET_HDAU
   add constraint FK_CHITIET__CHITIET_H_MATHANG foreign key (MA_MH)
      references MATHANG (MA_MH)
go

alter table CHITIET_HDDC
   add constraint FK_CHITIET__CHITIET_H_HOADONDI foreign key (MA_HDDC)
      references HOADONDICHUYEN (MA_HDDC)
go

alter table CHITIET_HDDC
   add constraint FK_CHITIET__CHITIET_H_CHUYENDI foreign key (MA_CD)
      references CHUYENDI (MA_CD)
go

alter table CHITIET_HDGU
   add constraint FK_CHITIET__CHITIET_H_HOADONGI foreign key (MA_HDGU)
      references HOADONGIATUI (MA_HDGU)
go

alter table CHITIET_HDGU
   add constraint FK_CHITIET__CHITIET_H_LUOTGIAT foreign key (MA_LUOTGU)
      references LUOTGIATUI (MA_LUOTGU)
go

alter table CHITIET_HDLT
   add constraint FK_CHITIET__CHITIET_H_HOADONLU foreign key (MA_HDLT)
      references HOADONLUUTRU (MA_HDLT)
go

alter table CHITIET_HDLT
   add constraint FK_CHITIET__CHITIET_H_PHONG foreign key (MA_PHONG)
      references PHONG (MA_PHONG)
go

alter table HOADON
   add constraint FK_HOADON_CO_HD_KHACHHAN foreign key (MA_KH)
      references KHACHHANG (MA_KH)
go

alter table HOADON
   add constraint FK_HOADON_CO_HDAU_HOADONAN foreign key (MA_HDAU)
      references HOADONANUONG (MA_HDAU)
go

alter table HOADON
   add constraint FK_HOADON_CO_HDDC_HOADONDI foreign key (MA_HDDC)
      references HOADONDICHUYEN (MA_HDDC)
go

alter table HOADON
   add constraint FK_HOADON_CO_HDGU_HOADONGI foreign key (MA_HDGU)
      references HOADONGIATUI (MA_HDGU)
go

alter table HOADON
   add constraint FK_HOADON_CO_HDLT_HOADONLU foreign key (MA_HDLT)
      references HOADONLUUTRU (MA_HDLT)
go

alter table HOADON
   add constraint FK_HOADON_LAP_HD_NHANVIEN foreign key (MA_NV)
      references NHANVIEN (MA_NV)
go

alter table LUOTGIATUI
   add constraint FK_LUOTGIAT_CO_LOAIGU_LOAIGIAT foreign key (MA_LOAIGU)
      references LOAIGIATUI (MA_LOAIGU)
go

alter table NHANVIEN
   add constraint FK_NHANVIEN_CO_TK_TAIKHOAN foreign key (MA_TK)
      references TAIKHOAN (MA_TK)
go

alter table PHONG
   add constraint FK_PHONG_CO_LP_LOAIPHON foreign key (MA_LP)
      references LOAIPHONG (MA_LP)
go

