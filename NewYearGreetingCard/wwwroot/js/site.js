// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const copyFeedbackTimeouts = new WeakMap();

function showCopyFeedback(feedbackElement, message, isFallback) {
    if (!feedbackElement) {
        return;
    }

    feedbackElement.textContent = message;
    feedbackElement.classList.toggle("is-fallback", Boolean(isFallback));
    feedbackElement.classList.add("is-visible");

    const existingTimeout = copyFeedbackTimeouts.get(feedbackElement);
    if (existingTimeout) {
        window.clearTimeout(existingTimeout);
    }

    const timeoutId = window.setTimeout(() => {
        feedbackElement.classList.remove("is-visible");
    }, 2000);

    copyFeedbackTimeouts.set(feedbackElement, timeoutId);
}

function fallbackCopy(text, feedbackElement) {
    const textarea = document.createElement("textarea");
    textarea.value = text;
    textarea.setAttribute("readonly", "");
    textarea.style.position = "fixed";
    textarea.style.opacity = "0";
    textarea.style.top = "0";
    textarea.style.left = "0";
    document.body.appendChild(textarea);
    textarea.select();

    let copied = false;
    try {
        copied = document.execCommand("copy");
    } catch (error) {
        copied = false;
    }

    document.body.removeChild(textarea);

    if (copied) {
        showCopyFeedback(feedbackElement, "已複製！", false);
    } else {
        showCopyFeedback(feedbackElement, "請按 Ctrl+C 複製", true);
    }
}

async function copyToClipboard(text, feedbackElement) {
    if (!text) {
        return;
    }

    try {
        if (navigator.clipboard && window.isSecureContext) {
            await navigator.clipboard.writeText(text);
            showCopyFeedback(feedbackElement, "已複製！", false);
            return;
        }
    } catch (error) {
        // Fall back to execCommand for older browsers or blocked clipboard access.
    }

    fallbackCopy(text, feedbackElement);
}

document.addEventListener("click", (event) => {
    const target = event.target;
    if (!(target instanceof Element)) {
        return;
    }

    const copyButton = target.closest(".greeting-copy-button");
    if (!copyButton) {
        return;
    }

    const text = copyButton.getAttribute("data-copy-text");
    if (!text) {
        return;
    }

    const feedbackId = copyButton.getAttribute("data-copy-feedback");
    const feedbackElement = feedbackId
        ? document.getElementById(feedbackId)
        : copyButton.closest(".greeting-message")?.querySelector(".greeting-copy-feedback");

    void copyToClipboard(text, feedbackElement);
});
