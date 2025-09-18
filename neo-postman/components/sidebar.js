// components/sidebar.js
const template = document.createElement('template');
template.innerHTML = `
  <div class="side-sticky">
    <div class="side-head">
      <strong>Collections Manager</strong>

      <div class="dropdown">
        <button class="btn btn-accent" id="actionsBtn" aria-haspopup="true" aria-expanded="false">
          <svg class="ic" viewBox="0 0 24 24"><path fill="currentColor" d="M12 7l5 5-5 5-5-5z"/></svg>
          Actions
        </button>

        <!-- Menu chính: căn trái nút, đổ sang phải -->
        <div class="dropdown-menu" id="actionsDropdown" role="menu" aria-hidden="true">
          <!-- NEW (tree) -->
          <button class="menu-item has-sub" id="miNew" role="menuitem" aria-haspopup="true" aria-expanded="false">
            <span>New</span>
            <span class="caret">▸</span>
          </button>
          <div class="dropdown-sub" id="subNew" role="menu" aria-hidden="true">
            <button class="menu-item" data-new="request" role="menuitem">Request</button>
            <button class="menu-item" data-new="collection" role="menuitem">Collection</button>
            <button class="menu-item" data-new="folder" role="menuitem">Folder</button>
            <button class="menu-item" data-new="env" role="menuitem">Environment</button>
          </div>

          <div class="menu-sep" role="separator"></div>

          <!-- Expand/Collapse All (tự đổi nhãn) -->
          <button class="menu-item" id="miToggleAll" role="menuitem">
            Collapse All
          </button>

          <div class="menu-sep" role="separator"></div>

          <button class="menu-item" id="miImport" role="menuitem">Import</button>
          <button class="menu-item" id="miExport" role="menuitem">Export</button>
        </div>
      </div>
    </div>

    <div class="side-search">
      <input id="searchCollections" placeholder="Search collections…" aria-label="Search">
    </div>

    <input type="radio" name="stab" id="stab1" checked>
    <input type="radio" name="stab" id="stab2">
    <input type="radio" name="stab" id="stab3">
    <div class="tabs" role="tablist" aria-label="Sidebar tabs">
      <label class="t1" for="stab1" role="tab" aria-controls="tabCollections">Collections</label>
      <label class="t2" for="stab2" role="tab" aria-controls="tabEnvs">Environments</label>
      <label class="t3" for="stab3" role="tab" aria-controls="tabAutoTest">Auto Testing</label>
    </div>
  </div>

  <div class="side-list" id="collectionList" aria-live="polite">
    <section class="collection" data-key="Auth Service" data-open="1">
      <div class="c-head">
        <button class="c-toggle" title="Toggle section" aria-label="Toggle Auth Service">
          <svg viewBox="0 0 24 24"><path fill="currentColor" d="M8 10l4 4 4-4z"/></svg>
        </button>
        Auth Service
      </div>
      <div class="c-body">
        <div class="req"><span class="method POST">POST</span><span>/api/auth/login</span></div>
        <div class="req"><span class="method GET">GET</span><span>/api/auth/profile</span></div>
        <div class="req"><span class="method DEL">DEL</span><span>/api/auth/logout</span></div>
      </div>
    </section>

    <section class="collection" data-key="Products" data-open="1">
      <div class="c-head">
        <button class="c-toggle" title="Toggle section" aria-label="Toggle Products">
          <svg viewBox="0 0 24 24"><path fill="currentColor" d="M8 10l4 4 4-4z"/></svg>
        </button>
        Products
      </div>
      <div class="c-body">
        <div class="req active"><span class="method GET">GET</span><span>/api/products</span></div>
        <div class="req"><span class="method POST">POST</span><span>/api/products</span></div>
        <div class="req"><span class="method PUT">PUT</span><span>/api/products/:id</span></div>
      </div>
    </section>
  </div>
`;

class NeoSidebar extends HTMLElement {
  constructor() { super(); this.attachShadow({ mode: 'open' }); }

  async connectedCallback() {
    this.shadowRoot.appendChild(template.content.cloneNode(true));
    // inject CSS
    const cssHref = new URL('./sidebar.css', import.meta.url);
    const cssText = await fetch(cssHref).then(r => r.text());
    const styleTag = document.createElement('style'); styleTag.textContent = cssText;
    this.shadowRoot.prepend(styleTag);

    this.initDropdown();
    this.initCollections();
    this.initSearch();
  }

  /* ========== Dropdown (left-aligned + tree for New) ========== */
initDropdown() {
  const btn = this.shadowRoot.getElementById('actionsBtn');
  const menu = this.shadowRoot.getElementById('actionsDropdown');
  const miNew = this.shadowRoot.getElementById('miNew');
  const subNew = this.shadowRoot.getElementById('subNew');
  const miToggleAll = this.shadowRoot.getElementById('miToggleAll');
  const miImport = this.shadowRoot.getElementById('miImport');
  const miExport = this.shadowRoot.getElementById('miExport');

  const openMenu = () => {
    menu.classList.add('open');
    menu.setAttribute('aria-hidden', 'false');
    btn.setAttribute('aria-expanded', 'true');
  };
  const closeMenu = () => {
    menu.classList.remove('open');
    menu.setAttribute('aria-hidden', 'true');
    btn.setAttribute('aria-expanded', 'false');
    closeSub();
  };
  const toggleMenu = () => {
    menu.classList.contains('open') ? closeMenu() : openMenu();
  };

  const openSub = () => {
    subNew.classList.add('open');
    subNew.setAttribute('aria-hidden', 'false');
    miNew.setAttribute('aria-expanded', 'true');
  };
  const closeSub = () => {
    subNew.classList.remove('open');
    subNew.setAttribute('aria-hidden', 'true');
    miNew.setAttribute('aria-expanded', 'false');
  };
  const toggleSub = () => {
    subNew.classList.contains('open') ? closeSub() : openSub();
  };

  // Click nút → mở/đóng menu
  btn.addEventListener('click', (e) => {
    e.stopPropagation();
    toggleMenu();
  });

  // Click New → mở/đóng submenu
  miNew.addEventListener('click', (e) => {
    e.stopPropagation();
    toggleSub();
  });

  // Click ngoài → đóng tất cả
  document.addEventListener('click', (e) => {
    if (!this.contains(e.target)) closeMenu();
  });
  document.addEventListener('keydown', (e) => {
    if (e.key === 'Escape') closeMenu();
  });

  // Các menu item
  subNew.querySelectorAll('.menu-item').forEach(it => {
    it.addEventListener('click', () => {
      const type = it.dataset.new; // request/collection/folder/env
      this.dispatchEvent(new CustomEvent('sidebar-new', { detail: { type }, bubbles: true }));
      alert(`New ${type} (mock)`);
      closeMenu();
    });
  });

  miToggleAll.addEventListener('click', () => {
    const collections = [...this.shadowRoot.querySelectorAll('.collection')];
    const anyOpen = collections.some(c => c.getAttribute('data-open') !== '0');
    collections.forEach(col => {
      const key = col.dataset.key || '';
      const sk = 'neo.collection.open:' + key;
      const body = col.querySelector('.c-body');
      col.setAttribute('data-open', anyOpen ? '0' : '1');
      localStorage.setItem(sk, anyOpen ? '0' : '1');
      this._animateBody(body, !anyOpen);
    });
    miToggleAll.textContent = anyOpen ? 'Expand All' : 'Collapse All';
    this.dispatchEvent(new CustomEvent('collections-collapsed', { detail: { collapsed: anyOpen }, bubbles: true }));
    closeMenu();
  });

  miImport.addEventListener('click', () => {
    this.dispatchEvent(new CustomEvent('sidebar-import', { bubbles: true }));
    alert('Import collections (mock)');
    closeMenu();
  });
  miExport.addEventListener('click', () => {
    this.dispatchEvent(new CustomEvent('sidebar-export', { bubbles: true }));
    alert('Export collections (mock)');
    closeMenu();
  });
}


  /* ========== Collections ========== */
  _animateBody(body, open) {
    if (open) {
      body.style.display = 'block';
      const h = body.scrollHeight;
      body.style.maxHeight = '0px';
      requestAnimationFrame(() => { body.style.maxHeight = h + 'px'; });
      const onEnd = () => { body.style.maxHeight = 'none'; body.removeEventListener('transitionend', onEnd); };
      body.addEventListener('transitionend', onEnd);
    } else {
      const h = body.scrollHeight;
      body.style.maxHeight = h + 'px';
      requestAnimationFrame(() => { body.style.maxHeight = '0px'; });
      const onEnd = () => { body.style.display = 'none'; body.removeEventListener('transitionend', onEnd); };
      body.addEventListener('transitionend', onEnd);
    }
  }

  initCollections() {
    const list = this.shadowRoot.getElementById('collectionList');
    const collections = [...list.querySelectorAll('.collection')];

    collections.forEach(col => {
      const key = col.dataset.key || Math.random().toString(36).slice(2);
      const sk = 'neo.collection.open:' + key;
      const saved = localStorage.getItem(sk);
      if (saved !== null) col.setAttribute('data-open', saved);

      const body = col.querySelector('.c-body');
      const isOpenInit = col.getAttribute('data-open') !== '0';
      if (!isOpenInit) { body.style.display = 'none'; body.style.maxHeight = '0px'; }

      col.querySelector('.c-toggle')?.addEventListener('click', () => {
        const isOpen = col.getAttribute('data-open') !== '0';
        col.setAttribute('data-open', isOpen ? '0' : '1');
        localStorage.setItem(sk, isOpen ? '0' : '1');
        this._animateBody(body, !isOpen);
        this.dispatchEvent(new CustomEvent('collection-toggled', { detail: { key, open: !isOpen }, bubbles: true }));
      });
    });
  }

  /* ========== Search ========== */
  initSearch() {
    const input = this.shadowRoot.getElementById('searchCollections');
    const collections = [...this.shadowRoot.querySelectorAll('.collection')];
    input.addEventListener('input', (e) => {
      const q = e.target.value.toLowerCase().trim();
      collections.forEach(col => {
        const key = (col.dataset.key || '').toLowerCase();
        col.style.display = key.includes(q) ? '' : 'none';
      });
    });
  }
}

customElements.define('neo-sidebar', NeoSidebar);
