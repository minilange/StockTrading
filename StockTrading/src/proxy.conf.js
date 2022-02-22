const PROXY_CONFIG = [
  {
    context: [
      "/stock",
      "/transaction",
      "/history",
    ],
    target: "https://localhost:7017",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
