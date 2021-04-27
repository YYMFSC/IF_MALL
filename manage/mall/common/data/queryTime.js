var ajax = require('../../common/ajax/ajax.js'),
  config = require('../../common/Data/config.js');

function query(e){
ajax.query({
  url: config.IF_Url,
  param: { method: "GettimeMessage" },
  callback: function (res) {
    for(var i=0;i<res.Table.length;i++)
    {
      if(res.Table[i].data==e)
      {
         return res.Table[i].time;
      }
    }
  }
})
}

module.exports = {
  query: query
}