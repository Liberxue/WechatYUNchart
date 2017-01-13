/*
 Navicat Premium Data Transfer

 Source Server         : oscwifi
 Source Server Type    : MySQL
 Source Server Version : 50547
 Source Host           : 114.215.30.96
 Source Database       : yunkufu

 Target Server Type    : MySQL
 Target Server Version : 50547
 File Encoding         : utf-8

 Date: 01/13/2017 18:35:55 PM
*/

SET NAMES utf8;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
--  Table structure for `accounttradedetail`
-- ----------------------------
DROP TABLE IF EXISTS `accounttradedetail`;
CREATE TABLE `accounttradedetail` (
  `orderno` varchar(255) DEFAULT NULL,
  `tradeno` varchar(255) DEFAULT NULL,
  `amount` float DEFAULT NULL,
  `tradetype` bigint(20) DEFAULT NULL,
  `currentbalance` float DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `tradetime` datetime DEFAULT NULL,
  `remark` varchar(255) DEFAULT NULL,
  `expandparams` varchar(255) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
--  Table structure for `adminuser`
-- ----------------------------
DROP TABLE IF EXISTS `adminuser`;
CREATE TABLE `adminuser` (
  `userid` bigint(20) DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `isactive` tinyint(1) DEFAULT NULL,
  `accountbalance` float DEFAULT NULL,
  `issupermanage` tinyint(1) DEFAULT NULL,
  `truename` varchar(255) DEFAULT NULL,
  `mobile` varchar(255) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `remark` varchar(255) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
--  Records of `adminuser`
-- ----------------------------
BEGIN;
INSERT INTO `adminuser` VALUES ('2', 'admin', 'qkNqQ1eGbhSsQYk7v4ECGg==', '1', '10000', '0', '013', '', '013', '                                                                                                                                                                                                                                                               ', '2017-01-31 00:00:00'), ('2', 'admin', 'qkNqQ1eGbhSsQYk7v4ECGg==', '1', '10000', '0', '013', '', '013', '                                                                                                                                                                                                                                                               ', '2017-01-31 00:00:00');
COMMIT;

-- ----------------------------
--  Table structure for `dbconfig`
-- ----------------------------
DROP TABLE IF EXISTS `dbconfig`;
CREATE TABLE `dbconfig` (
  `configkey` varchar(255) DEFAULT NULL,
  `value` varchar(255) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
--  Table structure for `dblog`
-- ----------------------------
DROP TABLE IF EXISTS `dblog`;
CREATE TABLE `dblog` (
  `dblogid` bigint(20) DEFAULT NULL,
  `applicationtype` bigint(20) DEFAULT NULL,
  `logtypes` bigint(20) DEFAULT NULL,
  `primarykeydata` varchar(255) DEFAULT NULL,
  `subject` varchar(255) DEFAULT NULL,
  `logcontent` longtext,
  `username` varchar(255) DEFAULT NULL,
  `ip` varchar(255) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
--  Table structure for `keywordreply`
-- ----------------------------
DROP TABLE IF EXISTS `keywordreply`;
CREATE TABLE `keywordreply` (
  `keywordid` bigint(20) DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `robotid` varchar(255) DEFAULT NULL,
  `keyword` varchar(255) DEFAULT NULL,
  `matchingmode` bigint(20) DEFAULT NULL,
  `replycontent` longtext,
  `replycontenttype` bigint(20) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
--  Records of `keywordreply`
-- ----------------------------
BEGIN;
INSERT INTO `keywordreply` VALUES ('3', 'admin', '013', '签到', '0', '121212', '0', '2018-12-20 21:20:46'), ('6', 'admin', '013', '上班', '1', '我爱上班哈[得意]哈哈哈', '0', '2016-12-20 21:21:56'), ('7', 'admin', '013', '1', '1', '1323', '1', '2018-12-20 11:58:57'), ('8', 'admin', '013', '013', '0', '请点击[色][色][色]', '0', '2018-12-20 18:47:37'), ('9', 'admin', '013', '123', '0', '请点击013', '0', '2018-12-20 12:01:22'), ('3', 'admin', '013', '签到', '0', '签到成功', '0', '2018-12-20 21:20:46'), ('6', 'admin', '013', '上班', '1', '我爱上班哈[得意]哈哈哈', '0', '2018-12-20 21:21:56'), ('7', 'admin', '013', 'ces', '0', '1323', '0', '2018-12-20 11:58:57'), ('8', 'admin', '013', '013', '0', '请点击[色][色][色]', '0', '2016-12-20 18:47:37'), ('9', 'admin', '013', '123', '0', '请点击013', '0', '2016-12-20 12:01:22');
COMMIT;

-- ----------------------------
--  Table structure for `sysdiagrams`
-- ----------------------------
DROP TABLE IF EXISTS `sysdiagrams`;
CREATE TABLE `sysdiagrams` (
  `name` varchar(255) DEFAULT NULL,
  `principal_id` bigint(20) DEFAULT NULL,
  `diagram_id` bigint(20) DEFAULT NULL,
  `version` bigint(20) DEFAULT NULL,
  `definition` longblob
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
--  Table structure for `wechatadvertisement`
-- ----------------------------
DROP TABLE IF EXISTS `wechatadvertisement`;
CREATE TABLE `wechatadvertisement` (
  `adverid` bigint(20) DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `robotid` varchar(255) DEFAULT NULL,
  `advercategory` bigint(20) DEFAULT NULL,
  `advercontent` longtext,
  `replycontenttype` bigint(20) DEFAULT NULL,
  `sendmode` bigint(20) DEFAULT NULL,
  `sendmodeparas` varchar(255) DEFAULT NULL,
  `begintime` datetime DEFAULT NULL,
  `endtime` datetime DEFAULT NULL,
  `lastsendtime` datetime DEFAULT NULL,
  `createtime` datetime DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
--  Records of `wechatadvertisement`
-- ----------------------------
BEGIN;
INSERT INTO `wechatadvertisement` VALUES ('1', 'admin', '013', '1', '你好 我是013[em_13]  哈哈哈[惊恐]  这是一条测试定时消息1', '2', '1', '1', '2017-01-11 00:00:00', '2017-01-11 11:38:00', '2016-12-20 12:51:35', '2018-12-19 16:53:52');
COMMIT;

-- ----------------------------
--  Table structure for `wechatgroupmember`
-- ----------------------------
DROP TABLE IF EXISTS `wechatgroupmember`;
CREATE TABLE `wechatgroupmember` (
  `wechatgroupid` varchar(255) DEFAULT NULL,
  `groupmemberwechatid` varchar(255) DEFAULT NULL,
  `inviterwechatid` varchar(255) DEFAULT NULL,
  `nickname` varchar(255) DEFAULT NULL,
  `headimgurl` varchar(255) DEFAULT NULL,
  `jointime` datetime DEFAULT NULL,
  `groupmemberlevel` bigint(20) DEFAULT NULL,
  `lastactivetime` datetime DEFAULT NULL,
  `status` varchar(255) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
--  Table structure for `wechatrobot`
-- ----------------------------
DROP TABLE IF EXISTS `wechatrobot`;
CREATE TABLE `wechatrobot` (
  `robotid` varchar(255) DEFAULT NULL,
  `wechatid` varchar(255) DEFAULT NULL,
  `wxuin` varchar(255) DEFAULT NULL,
  `nickname` varchar(255) DEFAULT NULL,
  `headimgurl` varchar(255) DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `begindate` datetime DEFAULT NULL,
  `enddate` datetime DEFAULT NULL,
  `maxwxgroupcount` bigint(20) DEFAULT NULL,
  `currentwxgroupcount` bigint(20) DEFAULT NULL,
  `chatswitch` tinyint(1) DEFAULT NULL,
  `welcomeswitch` bigint(20) DEFAULT NULL,
  `redpacketswitch` bigint(20) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
--  Records of `wechatrobot`
-- ----------------------------
BEGIN;
INSERT INTO `wechatrobot` VALUES ('013', '013013', '1262787126', '○¹³', 'https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgeticon?seq=636721984&username=@9b9fc832f3dcc787e9f62eff6e0a9fa2d5b958d0dd3c8c205f2bf6c7b371744c&skey=@crypt_fe7e13d_072646f3fe7b8f131706e232556db049', 'admin', '2016-12-20 16:50:00', '2018-12-30 16:16:00', '10', '5', '2', '1', '2', '2017-12-17 00:00:00'), ('013', 'admin', '120353907', '陈莹', '', 'admin', '2016-12-20 00:00:00', '2018-08-02 00:00:00', '20', '0', '1', '2', '2', '2018-01-04 00:00:00'), ('013', '013013', '', '陈琳', 'https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgeticon?seq=636721984&username=@9b9fc832f3dcc787e9f62eff6e0a9fa2d5b958d0dd3c8c205f2bf6c7b371744c&skey=@crypt_fe7e13d_072646f3fe7b8f131706e232556db049', 'admin', '2016-12-20 00:00:00', '2018-07-31 00:00:00', '10', '5', '1', '1', '1', '2017-05-17 00:00:00'), ('013', 'admin', '1261626767', '王艳', '', 'admin', '2016-12-20 00:00:00', '2018-08-02 00:00:00', '20', '0', '1', '2', '2', '2017-01-04 00:00:00'), ('013', 'admin', '3446321462', '上官云', '', 'admin', '2016-12-20 00:00:00', '2018-08-02 00:00:00', '20', '0', '1', '2', '2', '2017-01-04 00:00:00'), ('013', 'admin', '3354326374', '晓慧', '', 'admin', '2016-12-20 00:00:00', '2018-08-02 00:00:00', '20', '0', '1', '2', '2', '2017-01-04 00:00:00');
COMMIT;

-- ----------------------------
--  Table structure for `welcome`
-- ----------------------------
DROP TABLE IF EXISTS `welcome`;
CREATE TABLE `welcome` (
  `wecid` bigint(20) DEFAULT NULL,
  `robotid` varchar(255) DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `welcome` longtext,
  `wectype` bigint(20) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
--  Records of `welcome`
-- ----------------------------
BEGIN;
INSERT INTO `welcome` VALUES ('14', '013', '013', '欢迎欢迎[微笑]！热烈欢迎！[鼓掌][鼓掌]新人进群，执行群规！上照片，爆三围！速度，速度！', '0', '2018-12-20 03:02:44');
COMMIT;

-- ----------------------------
--  Table structure for `wx_im_mess`
-- ----------------------------
DROP TABLE IF EXISTS `wx_im_mess`;
CREATE TABLE `wx_im_mess` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `optdt` datetime DEFAULT NULL,
  `zt` tinyint(1) DEFAULT '0' COMMENT '状态',
  `cont` varchar(4000) DEFAULT NULL COMMENT '内容',
  `sendid` smallint(6) DEFAULT NULL,
  `receid` smallint(6) DEFAULT '0' COMMENT '接收',
  `receuid` varchar(1000) DEFAULT NULL COMMENT '接收用户id',
  `type` varchar(20) DEFAULT NULL COMMENT '信息类型',
  `url` varchar(1000) DEFAULT NULL COMMENT '相关地址',
  `fileid` int(11) NOT NULL DEFAULT '0' COMMENT '对应文件Id',
  `msgid` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `optdt` (`optdt`,`receid`),
  KEY `msgid` (`msgid`)
) ENGINE=MyISAM AUTO_INCREMENT=28 DEFAULT CHARSET=utf8 COMMENT='IM聊天记录表';

-- ----------------------------
--  Records of `wx_im_mess`
-- ----------------------------
BEGIN;
INSERT INTO `wx_im_mess` VALUES ('1', '2016-09-08 23:38:17', '1', '5qyi6L!O5L2.55So77yM5pyJ5ZWl6Zeu6aKY6L!Z6YeM6K!05ZCn77yB', '8', '2', '2,5,6,7,3,4,8,9', 'group', null, '0', null), ('2', '2016-09-12 19:19:46', '1', 'MTI:', '4', '4', '4,4', 'user', null, '0', null), ('3', '2016-10-17 19:51:45', '1', 'W!WbvueJhyA0LjY1IEtCXQ::', '1', '1', '1,1', 'user', null, '2', null), ('4', '2016-10-17 19:54:24', '1', 'W!WbvueJhyA0Ljc5IEtCXQ::', '1', '1', '1,1', 'user', null, '3', null), ('5', '2016-10-17 19:57:15', '1', 'W!WbvueJhyA4Ni42MSBLQl0:', '1', '1', '1,1', 'user', null, '4', null), ('6', '2016-10-17 20:24:25', '1', 'W!WbvueJhyAxMi4xMSBLQl0:', '1', '1', '1,1', 'user', null, '5', null), ('7', '2016-12-01 00:43:27', '1', 'W!WbvueJhyA1OC4yMyBLQl0:', '9', '2', '1,2,5,6,7,3,4,8,9', 'group', null, '22', null), ('8', '2016-12-01 00:43:30', '1', 'W!WPkeWRhl0:', '9', '2', '1,2,5,6,7,3,4,8,9', 'group', null, '0', null), ('9', '2016-12-01 01:19:59', '1', 'Pw::', '9', '9', '9,9', 'user', null, '0', null), ('10', '2016-12-01 01:48:26', '1', 'W!WbsF0:', '9', '9', '9,9', 'user', null, '0', null), ('11', '2016-12-01 02:42:16', '1', 'ZG9n', '9', '9', '9,9', 'user', null, '0', null), ('12', '2016-12-01 02:42:19', '1', 'ZG9nIA::', '9', '9', '9,9', 'user', null, '0', null), ('13', '2016-12-01 02:42:32', '1', '5pOm5pOmIOe7iOS6juWlveS6huOAgg::', '9', '9', '9,9', 'user', null, '0', null), ('14', '2016-12-01 02:42:36', '1', '6aWt6aaG', '9', '9', '9,9', 'user', null, '0', null), ('15', '2016-12-01 02:42:41', '1', '56KO6KeJ', '9', '2', '1,2,5,6,7,3,4,8,9', 'group', null, '0', null), ('16', '2016-12-01 02:43:52', '1', 'Pw::', '9', '2', '1,2,5,6,7,3,4,8,9', 'group', null, '0', null), ('17', '2016-12-01 02:43:56', '1', 'Pw::', '9', '9', '9,9', 'user', null, '0', null), ('18', '2016-12-01 02:43:58', '1', 'Pw::', '9', '9', '9,9', 'user', null, '0', null), ('19', '2016-12-01 02:44:21', '1', 'W!mXreWYtF0:', '9', '9', '9,9', 'user', null, '0', null), ('20', '2016-12-01 02:44:30', '1', '5ZOI5ZOI5ZOI5ZOI', '9', '9', '9,9', 'user', null, '0', null), ('21', '2016-12-01 02:47:13', '1', 'Pz8:', '9', '9', '9,9', 'user', null, '0', null), ('22', '2016-12-01 02:47:22', '1', 'W!aKseaLs10:', '9', '9', '9,9', 'user', null, '0', null), ('23', '2016-12-23 13:34:16', '1', 'MjYxMg::', '9', '9', '9,9', 'user', null, '0', null), ('24', '2016-12-23 13:34:20', '1', 'NDU0NQ::', '9', '9', '9,9', 'user', null, '0', null), ('25', '2016-12-24 15:51:35', '1', 'MjM0MjM0MjM0', '9', '2', '1,2,5,6,7,3,4,8,9', 'group', null, '0', null), ('26', '2016-12-24 15:51:37', '1', 'MjM0M2I:', '9', '2', '1,2,5,6,7,3,4,8,9', 'group', null, '0', null), ('27', '2016-12-24 15:51:42', '1', 'MzQgW!WPkeWRhl0:', '9', '2', '1,2,5,6,7,3,4,8,9', 'group', null, '0', null);
COMMIT;

-- ----------------------------
--  Table structure for `wxchartlog`
-- ----------------------------
DROP TABLE IF EXISTS `wxchartlog`;
CREATE TABLE `wxchartlog` (
  `id` int(255) NOT NULL AUTO_INCREMENT,
  `wxuin` int(255) DEFAULT NULL,
  `username` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `City` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `HeadImgUrl` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `NickName` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `Province` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `PYQuanPin` varchar(200) COLLATE utf8_bin DEFAULT NULL,
  `RemarkName` varchar(200) COLLATE utf8_bin DEFAULT NULL,
  `RemarkPYQuanPin` varchar(200) COLLATE utf8_bin DEFAULT NULL,
  `Sex` int(255) DEFAULT NULL,
  `Signature` varchar(200) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=76560 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
--  Records of `wxchartlog`
-- ----------------------------
BEGIN;
INSERT INTO `wxchartlog` VALUES ('7998', '1262787126', '@c805046008fcbea01ea8dea8965521ff9bf0a44b8c1144c1c9b8b3d03733a661', '', '/cgi-bin/mmwebwx-bin/webwxgeticon?seq=642514302&username=@c805046008fcbea01ea8dea8965521ff9bf0a44b8c1144c1c9b8b3d03733a661&skey=', '默然……', '', 'moran', '黄炫e', 'huangxuane', '1', '→_→！'), ('7999', '1262787126', '@f3fe58bdc74b78bb17b0d0111d2e04c4f4f84f4d48020018d526c4f2ab3f26d4', '陇南', '/cgi-bin/mmwebwx-bin/webwxgeticon?seq=630861416&username=@f3fe58bdc74b78bb17b0d0111d2e04c4f4f84f4d48020018d526c4f2ab3f26d4&skey=', '雪浪', '甘肃', 'xuelang', '101广告公司', '101guanggaogongsi', '1', '随风'), ('8000', '1262787126', '@bd4de7ea7605bec9e0dce339e0485b7f', '深圳', '/cgi-bin/mmwebwx-bin/webwxgeticon?seq=642513812&username=@bd4de7ea7605bec9e0dce339e0485b7f&skey=', '顺丰速运', '广东', 'shunfengsuyun', '', '', '0', '最便捷的顺丰速运自助服务平台。下单寄件， 及时追踪快件状态，主动推送路由信息，同时订单管理、地址簿管理让您放心、舒心。我们一直在努力！'), ('8001', '1262787126', '@7ca1c4483cf285173facec9813f3a596', '福州', '/cgi-bin/mmwebwx-bin/webwxgeticon?seq=648389770&username=@7ca1c4483cf285173facec9813f3a596&skey=', '永辉超市', '福建', 'yonghuichaoshi', '', '', '0', '永辉超市官方微信账号'), ('8002', '1262787126', '@e18b6c577fe36e0d7ed69e62db517edbb2cd862da060d9e5f8426394081a10c0', '', '/cgi-bin/mmwebwx-bin/webwxgeticon?seq=639765578&username=@e18b6c577fe36e0d7ed69e62db517edbb2cd862da060d9e5f8426394081a10c0&skey=', '王有学', '', 'wangyouxue', '局长', 'juchang', '0', '');
COMMIT;

-- ----------------------------
--  Table structure for `wxchatcontent`
-- ----------------------------
DROP TABLE IF EXISTS `wxchatcontent`;
CREATE TABLE `wxchatcontent` (
  `id` int(255) NOT NULL AUTO_INCREMENT,
  `wxuin` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `chartUser` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `formname` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `toname` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `Chatcontent` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `CreateTime` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=17 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
--  Records of `wxchatcontent`
-- ----------------------------
BEGIN;
INSERT INTO `wxchatcontent` VALUES ('1', '1262787126', '文件传输助手', '○¹³', '文件传输助手', '说的放松的方式地方', '2017/1/11 16:41:32'), ('2', '1262787126', '文件传输助手', '○¹³', '文件传输助手', '收到一个系统数据', '2017/1/11 16:41:45'), ('3', '1262787126', '文件传输助手', '○¹³', '文件传输助手', '法国红酒经济', '2017/1/11 16:41:48'), ('4', '1262787126', '文件传输助手', '○¹³', '文件传输助手', '说的放松的方式地方', '2017/1/11 16:55:30'), ('5', '1262787126', '文件传输助手', '○¹³', '文件传输助手', '试试是 是是是', '2017/1/11 17:04:00'), ('6', '1262787126', '文件传输助手', '○¹³', '文件传输助手', '我问企鹅', '2017/1/11 17:18:18'), ('7', '1262787126', '文件传输助手', '○¹³', '文件传输助手', '测试快捷回复', '2017/1/11 17:26:00'), ('8', '1262787126', '文件传输助手', '○¹³', '文件传输助手', '12233223', '2017/1/11 17:42:18'), ('9', '1262787126', '文件传输助手', '○¹³', '文件传输助手', '爱上达到萨斯的', '2017/1/11 17:47:15'), ('10', '1262787126', '文件传输助手', '○¹³', '文件传输助手', '112123123231', '2017/1/11 18:02:37'), ('11', '1262787126', '林莹', '○¹³', '林莹', '1212312323123', '2017/1/11 18:02:58'), ('12', '1262787126', '林莹', '○¹³', '林莹', '21312123', '2017/1/11 18:03:02'), ('13', '1262787126', '林莹', '○¹³', '林莹', '013', '2017/1/11 18:03:04'), ('14', '1262787126', '林莹', '○¹³', '林莹', '013', '2017/1/11 18:03:05'), ('15', '1262787126', '文件传输助手', '○¹³', '文件传输助手', '112123123231', '2017/1/11 18:02:37'), ('16', '1262787126', '快递100', '快递100', '○¹³', '&lt;msg&gt;<br/>    &lt;appmsg appid=\"\" sdkver=\"0\"&gt;<br/>        &lt;title&gt;&lt;![CDATA[送个快递小程序给你用]]&gt;&lt;/title&gt;<br/>        &lt;des&gt;&lt;![CDATA[送个快递小程序给你用，快递小程序能做很多事...]]&gt;&lt;/des&gt;<br/>        &lt;action&gt;&lt;/action&gt;<br/>        ', '2017/1/11 18:05:13');
COMMIT;

-- ----------------------------
--  Table structure for `wxcheckin`
-- ----------------------------
DROP TABLE IF EXISTS `wxcheckin`;
CREATE TABLE `wxcheckin` (
  `checkinid` bigint(20) DEFAULT NULL,
  `wxuin` varchar(255) DEFAULT NULL,
  `attrstatus` bigint(20) DEFAULT NULL,
  `groupusername` varchar(255) DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `nickname` varchar(255) DEFAULT NULL,
  `lastcheckintime` datetime DEFAULT NULL,
  `todayrank` bigint(20) DEFAULT NULL,
  `monthrank` bigint(20) DEFAULT NULL,
  `totalcheckin` bigint(20) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
--  Table structure for `wxoperatelog`
-- ----------------------------
DROP TABLE IF EXISTS `wxoperatelog`;
CREATE TABLE `wxoperatelog` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `wxuin` varchar(255) DEFAULT NULL,
  `groupusername` varchar(255) DEFAULT NULL,
  `logcontent` varchar(255) DEFAULT NULL,
  `wxlogtype` bigint(20) DEFAULT NULL,
  `username` varchar(255) DEFAULT NULL,
  `nickname` varchar(255) DEFAULT NULL,
  `createtime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=781 DEFAULT CHARSET=utf8;

-- ----------------------------
--  Records of `wxoperatelog`
-- ----------------------------
BEGIN;
INSERT INTO `wxoperatelog` VALUES ('1', '1262787126', '@@40caa4da4cdac572a0fba1c74b9ef765f4b28a7fd1417bc59e7720d4b016f2bc', '你邀请\"福州移动  赵芳\"加入了群聊', '0', null, null, '2016-12-20 10:34:52'), ('2', '1262787126', '@@fc690d792f04e1cf0cc960348c8eebd1813f633e0b36a697d38c673eae896286', '你邀请\"福州移动  赵芳\"加入了群聊', '0', null, null, '2016-12-20 14:19:33'), ('3', '120353907', '@@2144e3391b2b206d4f7ab2f045db46c4796874c09d4c31ec9f98bff74f984ceb', '\"○¹³\"邀请\"福州移动杨秀云、福州移动 周慧、福州移动 陈闵\"加入了群聊', '0', null, null, '2016-12-20 14:20:47');
COMMIT;

-- ----------------------------
--  Table structure for `wxreply`
-- ----------------------------
DROP TABLE IF EXISTS `wxreply`;
CREATE TABLE `wxreply` (
  `id` int(11) NOT NULL DEFAULT '0',
  `content` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `userid` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
--  Records of `wxreply`
-- ----------------------------
BEGIN;
INSERT INTO `wxreply` VALUES ('2', '测试快捷回复', null), ('1', '013', null);
COMMIT;

-- ----------------------------
--  Table structure for `wxrogermsglog`
-- ----------------------------
DROP TABLE IF EXISTS `wxrogermsglog`;
CREATE TABLE `wxrogermsglog` (
  `id` int(255) NOT NULL AUTO_INCREMENT,
  `wxuin` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `username` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `LogContent` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `WxLogType` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `Wxfrom` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `Wxto` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `type` varchar(11) COLLATE utf8_bin DEFAULT NULL,
  `skey` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `CreateTime` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ----------------------------
--  Table structure for `wxsendmsglog`
-- ----------------------------
DROP TABLE IF EXISTS `wxsendmsglog`;
CREATE TABLE `wxsendmsglog` (
  `id` int(255) NOT NULL AUTO_INCREMENT,
  `wxuin` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `username` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `LogContent` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `WxLogType` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `Wxfrom` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `Wxto` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `type` varchar(11) COLLATE utf8_bin DEFAULT NULL,
  `skey` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  `CreateTime` varchar(255) COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

SET FOREIGN_KEY_CHECKS = 1;
