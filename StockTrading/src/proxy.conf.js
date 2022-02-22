const PROXY_CONFIG = [
  {
    context: [
      "/*"
    ],
    target: "https://localhost:7017/",
    secure: false,
    changeOrigin: true
  }
]

module.exports = PROXY_CONFIG;
