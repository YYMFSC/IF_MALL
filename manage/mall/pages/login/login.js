// pages/login/login.js

const app = getApp();
import {
    User
} from '../../common/model/user.js'
let user = new User();
var ajax = require('../../common/ajax/ajax.js'),
  config = require('../../common/data/config.js');
var Mcaptcha = require('../../common/utils/mcaptcha.js');
Page({

  /**
   * 页面的初始数据
   */
  data: {
    openid:'',
    name:'',
    imgCode:'',
    typeName: 'name', // 密码框的类型,用于显示密码时更改类型可看见输入的密码而非*号
    passFlag: 1,    // 密码第几次点击代表,用于显示不同的图标
    storePass: '',  // 暂存密码,用于显示密码
  },
  showPass() {     // 显示密码而非*号
    console.log(this.data.storePass,123)
    if (this.data.passFlag == 1) { // 第一次点击
      this.setData({ passFlag: 2, typeName: 'text' });
    } else {                        // 第二次点击
      this.setData({ passFlag: 1, typeName: 'name' });
    }
  },
  // 监听密码input框并把输入的密码在变量storePass下暂存起来,用于在显示密码操作展示
  getopenid: function (e) {
    this.setData({ openid: e.detail.value, storePass: e.detail.value, })
    console.log(this.data.openid);
  },
  getname: function (e) {
    this.setData({ name: e.detail.value })
    console.log(this.data.name);
  },

  codeImg: function (e) {
    this.setData({ imgCode: e.detail.value })
    console.log(this.data.imgCode);
  },
//表单提交
  loginClick:function(e){
    var errMs = '';
      var that = this;
      wx.login({
        success (res) {
          if (res.code) {
            //发起网络请求
            user.login({
              data: {
                  code: res.code
              }
          });
        }}
      })
    //验证验证码
    var res1 = that.mcaptcha.validate(that.data.imgCode);
    if (that.data.openid == "") {
      errMs = "请输入账号";
    }
    if (that.data.openid.length<1) {
      errMs = "账号有误";
    }
    else if (that.data.name == "") {
      errMs = "请输入密码";
    }
    /*else if (this.data.codeImg=="") {
      errMs = "请输入验证码";
    }*/
    else if(that.data.imgCode == '' || that.data.imgCode == null) {
        errMs= "请输入验证码";
    }
    
    if (errMs != "") {
      wx.showToast({
        title: errMs,
        icon: 'none',
        duration: 2000
      })
      that.onTap()
    }
    else{
      //测试登陆人员是否学校人员。
      ajax.query({
        url: config.IF_Url,
        param: { method: "GetxxMessage", openid: that.data.openid, name: that.data.name },
        callback: function (res) {
          console.log(res,res.Table)
          if(res.Table!="")
          {
            wx.setStorageSync('id', res.Table[0].Sfzh)
            console.log(res.Table[0].num, res.Table[0].Sfzh, "hh")
          if (res.Table[0].num != 0) {
            
            
            if (!res1) {
              wx.showToast({
                title: '验证码错误',
                icon: 'none',
                duration: 2000
              })
              that.onTap()
            }
            else{
              
              wx.showToast({
                title: '登录成功',
                duration: 2000,
              })
              /*wx.redirectTo({
                url: '../home/home',
              })*/
              that.first(res.Table[0].Sfzh)
            /*wx.switchTab({
              url: '../home/home'
            })*/
            }
            
          }
          }
          else {
              wx.showToast({
                title: '账号密码错误',
                icon: 'none',
                duration: 2000,
              })
              that.onTap()
            }
        }
        })
      }
    
    
    
  },
  
  first(sfzh){
    
    //测试是否第一次登录。
    ajax.query({
      url: config.IF_Url,
      param: { method: "GetpersonalMessage", sfzh:sfzh},
      callback: function (res) {
        console.log(res)
        if (res.Table[0] != null) {
          wx.switchTab({
            url: '../home/home'
          })
          
        }
        else{
          wx.redirectTo({
            url: '../home/home',
          })
        }
      }
    })
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    
  },
  
 

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {
    this.mcaptcha = new Mcaptcha({
      el: 'canvas',
      width: 80,
      height: 35,
      createCodeImg: ""
    });
  },
  //刷新验证码
  onTap() {
    this.mcaptcha.refresh();
  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})