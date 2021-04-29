const formatTime = date => {
  const year = date.getFullYear()
  const month = date.getMonth() + 1
  const day = date.getDate()
  const hour = date.getHours()
  const minute = date.getMinutes()
  const second = date.getSeconds()

  return [year, month, day].map(formatNumber).join('/') + ' ' + [hour, minute, second].map(formatNumber).join(':')
}

const formatNumber = n => {
  n = n.toString()
  return n[1] ? n : '0' + n
}
function formatterData(d){
  let arr = [];
    for (let k in d) {
      let s = {
        "Fieldname": k,
        "Fieldtext": "",
        "Value": d[k]
      }
      arr.push(s)
    }
    return JSON.stringify(arr)
}
module.exports = {
  formatTime: formatTime,
  formatterData:formatterData
}
