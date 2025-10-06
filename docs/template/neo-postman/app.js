(function () {
  const root = document.documentElement;

  /* ===== Resizer ===== */
  const resizer = document.getElementById('resizer');
  const LS_W = 'neo.sidebar.w';
  const savedW = parseInt(localStorage.getItem(LS_W), 10);
  if (!isNaN(savedW)) root.style.setProperty('--sidebar-w', savedW + 'px');

  let dragging = false, startX = 0, startW = 0;
  const clamp = (v, a, b) => Math.min(Math.max(v, a), b);

  const startDrag = (clientX) => {
    dragging = true;
    startX = clientX;
    startW = parseInt(getComputedStyle(root).getPropertyValue('--sidebar-w')) || 300;
    document.body.style.cursor = 'col-resize';
  };

  resizer.addEventListener('mousedown', (e) => startDrag(e.clientX));
  window.addEventListener('mousemove', (e) => {
    if (!dragging) return;
    const w = clamp(startW + (e.clientX - startX), 220, 560);
    root.style.setProperty('--sidebar-w', w + 'px');
  });
  window.addEventListener('mouseup', () => {
    if (!dragging) return;
    dragging = false;
    document.body.style.cursor = '';
    const w = parseInt(getComputedStyle(root).getPropertyValue('--sidebar-w')) || 300;
    localStorage.setItem(LS_W, String(w));
  });
  resizer.addEventListener('keydown', (e) => {
    if (e.key !== 'ArrowLeft' && e.key !== 'ArrowRight') return;
    const delta = e.key === 'ArrowLeft' ? -20 : 20;
    const cur = parseInt(getComputedStyle(root).getPropertyValue('--sidebar-w')) || 300;
    const w = clamp(cur + delta, 220, 560);
    root.style.setProperty('--sidebar-w', w + 'px');
    localStorage.setItem(LS_W, String(w));
    e.preventDefault();
  });

  /* ===== History view toggle ===== */
  const main = document.getElementById('main');
  const btnHistory = document.getElementById('btnHistory');
  const btnBackBuilder = document.getElementById('btnBackBuilder');
  const histBody = document.querySelector('#histTable tbody');

  const historyData = [
    { ts:new Date(Date.now()-1000*60*2), user:'You', method:'GET', url:'/api/products?limit=20', status:200 },
    { ts:new Date(Date.now()-1000*60*9), user:'You', method:'POST', url:'/api/auth/login', status:200 },
    { ts:new Date(Date.now()-1000*60*33), user:'Teammate A', method:'PUT', url:'/api/products/123', status:204 },
    { ts:new Date(Date.now()-1000*60*90), user:'Teammate B', method:'GET', url:'/api/auth/profile', status:401 }
  ];
  const pad = n => String(n).padStart(2,'0');
  const fmt = d => `${d.getFullYear()}-${pad(d.getMonth()+1)}-${pad(d.getDate())} ${pad(d.getHours())}:${pad(d.getMinutes())}:${pad(d.getSeconds())}`;
  function renderHistory(){
    histBody.innerHTML = historyData.map(h => `
      <tr>
        <td>${fmt(h.ts)}</td>
        <td>${h.user}</td>
        <td><span class="pill">${h.method}</span></td>
        <td class="mono">${h.url}</td>
        <td><span class="pill" style="color:${h.status>=200&&h.status<300?'#22c55e':'#ef4444'}">${h.status}</span></td>
      </tr>`).join('');
  }
  renderHistory();

  btnHistory.addEventListener('click', () => { main.setAttribute('data-mode', 'history'); });
  btnBackBuilder.addEventListener('click', () => { main.setAttribute('data-mode', 'builder'); });

  /* ===== Send mock ===== */
  const sendBtn = document.getElementById('sendBtn');
  const timeChip = document.getElementById('timeChip');
  const sizeChip = document.getElementById('sizeChip');
  const statusChip = document.getElementById('statusChip');
  const urlInput = document.getElementById('urlInput');
  const methodSelect = document.getElementById('methodSelect');
  const bodyInput = document.getElementById('bodyInput');

  sendBtn.addEventListener('click', () => {
    const t = Math.floor(30 + Math.random() * 120);
    const kb = (0.8 + Math.random() * 3).toFixed(2);
    const ok = Math.random() > 0.08;

    timeChip.textContent = t + ' ms';
    sizeChip.textContent = kb + ' KB';
    statusChip.textContent = ok ? '200 OK' : '500 ERROR';
    statusChip.className = 'chip ' + (ok ? 'ok' : '');

    const payload = {
      method: methodSelect.value,
      url: urlInput.value,
      body: bodyInput.innerText.trim() || null,
      took_ms: t,
      ok
    };
    document.getElementById('prettyOut').textContent = JSON.stringify(payload, null, 2);

    document.getElementById('rawOut').textContent = `HTTP/1.1 ${ok?200:500} ${ok?'OK':'ERROR'}
Content-Type: application/json
Content-Length: ${Math.round(kb*1024)}

${JSON.stringify({ message: ok?'Success':'Failure', at: new Date().toISOString() }, null, 2)}`;

    historyData.unshift({
      ts: new Date(), user: 'You',
      method: methodSelect.value, url: urlInput.value, status: ok ? 200 : 500
    });
    renderHistory();
  });

  /* ===== (Tùy chọn) Lắng nghe event từ sidebar ===== */
  document.addEventListener('sidebar-new', () => {/* hook nếu cần */});
  document.addEventListener('sidebar-import', () => {/* hook nếu cần */});
  document.addEventListener('sidebar-export', () => {/* hook nếu cần */});
})();
