Page({

  /**
   * 页面的初始数据
   */
  data: {
      title: '消息列表', // 状态
      list: [], // 数据列表
      type: '', // 数据类型
      loading: true // 显示等待框
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) { // options 为 board页传来的参数
      const _this = this;
      // 拼接请求url
      const url = 'http://localhost:58843/Controllers/MessageHandler.ashx?method=GetMSGList';
      // this.setData({
      //   list: [{
      //     id: 1,
      //     img: "https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fnimg.ws.126.net%2F%3Furl%3Dhttp%253A%252F%252Fdingyue.ws.126.net%252F2021%252F0404%252F11cdc498j00qr0xof002pd000hs00npp.jpg%26thumbnail%3D650x2147483647%26quality%3D80%26type%3Djpg&refer=http%3A%2F%2Fnimg.ws.126.net&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=jpeg?sec=1620489786&t=70d06d16adb851dcdcfc468937fc1341",
      //     neirong: "打打杀杀打算 爱仕达&nbsp; &nbsp;啊实打实&nbsp; &nbsp;< img src=\"/uploadfile/attached/image/20210502/20210502154546_7189.jpg\" width=\"111\" height=\"111\" title=\"111\" align=\"left\" alt=\"111\" /><span class=\"mini-textbox-border mini-corner-all\"></span>",
      //     price: "1111",
      //     title: "新品旗袍",
      //     uid: 111
      //   }],
      //   loading:false
      // })
      // 请求数据
      wx.request({
          url: url,
          data: { uid: "1111" },
          header: {
            'content-type': 'json'
          },
          success: function (res) {
              console.log(res.data);
              let data = res.data;
              _this.setData({
                  list: data,
                  loading: false
              })
          }
      })
  }
})